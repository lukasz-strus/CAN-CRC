using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using CAN_CRC.Extensions;
using CAN_CRC.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CAN_CRC.ViewModels;

// ReSharper disable InconsistentNaming
// ReSharper disable StringLiteralTypo
internal partial class MainViewModel : ObservableObject
{
    [ObservableProperty] private string? _input;
    [ObservableProperty] private string? _iteration;

    [ObservableProperty] private string? _resultCRC;
    [ObservableProperty] private string? _resultTime;
    [ObservableProperty] private string? _resultIterationTime;

    private readonly CRC _crc = new();

    [RelayCommand]
    private void Calculate()
    {
        if (InputIsNotValid() || IterationIsNotValid()) return;
        if (Input is null || Iteration is null) return;

        try
        {
            var inputBytes = Input.ToByteArray();
            var iterations = int.Parse(Iteration);
            var outputBytes = 0;
            var stopWatch = new Stopwatch();

            stopWatch.Start();
            for (var i = 0; i < iterations; i++)
            {
                outputBytes = _crc.Calculate(inputBytes);
            }
            stopWatch.Stop();
            
            ResultCRC = outputBytes.ToHex();

            decimal elapsedMilliseconds = stopWatch.ElapsedMilliseconds;
            ResultTime = elapsedMilliseconds.ToString(CultureInfo.CurrentCulture);
            var elapsedIterationMilliseconds = elapsedMilliseconds / iterations;
            ResultIterationTime = elapsedIterationMilliseconds.ToString("0.00000000");
        }
        catch
        {
            ShowCRCErrorMsg();
        }
    }
    

    private bool IterationIsNotValid()
    {
        if (string.IsNullOrEmpty(Iteration))
        {
            MessageBox.Show("Liczba iteracji jest pusta!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            return true;
        }
        var iteration = long.Parse(Iteration);
        if (iteration is >= 1 and <= 1000000000) return false;

        MessageBox.Show("Liczba iteracji musi być z zakresu 1-1000000000!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
        return true;
    }

    private bool InputIsNotValid()
    {
        if (string.IsNullOrEmpty(Input))
        {
            MessageBox.Show("Wejście jest puste!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            return true;
        }

        if (Input.Length <= 512) return false;

        MessageBox.Show("Wejście nie może być dłuższe niż 256 bajtów!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
        return true;
    }

    private static void ShowCRCErrorMsg() => MessageBox.Show("Wystąpił błąd podczas obliczania CRC!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);

}
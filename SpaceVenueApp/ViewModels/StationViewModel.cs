using System;
using System.Windows.Input;
using System.Windows.Media;
<<<<<<< HEAD
using System.Windows;
=======
>>>>>>> main

namespace SpaceVenueApp.ViewModels;

public class StationViewModel : ObservableObject
{
    private bool _isActive;
    private bool _isPaused;
    private TimeSpan _elapsed;
    private decimal _currentCharge;
<<<<<<< HEAD
    private string _customerName = string.Empty;
    private int? _sessionId;
=======
>>>>>>> main

    public StationViewModel(string name, string type, decimal ratePerHour)
    {
        Name = name;
        Type = type;
        RatePerHour = ratePerHour;
        StartCommand = new RelayCommand(StartSession, () => !IsActive || IsPaused);
        PauseCommand = new RelayCommand(PauseSession, () => IsActive && !IsPaused);
        StopCommand = new RelayCommand(StopSession, () => IsActive);
<<<<<<< HEAD
        ResetCommand = new RelayCommand(ResetSession, () => !IsActive && Elapsed > TimeSpan.Zero);
    }

    public event EventHandler<SessionEventArgs>? SessionStarted;
    public event EventHandler<SessionEventArgs>? SessionStopped;

=======
    }

>>>>>>> main
    public string Name { get; }
    public string Type { get; }
    public decimal RatePerHour { get; }

    public ICommand StartCommand { get; }
    public ICommand PauseCommand { get; }
    public ICommand StopCommand { get; }
<<<<<<< HEAD
    public ICommand ResetCommand { get; }
=======
>>>>>>> main

    public bool IsActive
    {
        get => _isActive;
        private set
        {
            if (SetProperty(ref _isActive, value))
            {
                RaisePropertyChanged(nameof(StatusBrush));
                RaiseCommandStates();
            }
        }
    }

    public bool IsPaused
    {
        get => _isPaused;
        private set
        {
            if (SetProperty(ref _isPaused, value))
            {
                RaiseCommandStates();
            }
        }
    }

    public TimeSpan Elapsed
    {
        get => _elapsed;
        private set
        {
            if (SetProperty(ref _elapsed, value))
            {
                RaisePropertyChanged(nameof(ElapsedDisplay));
<<<<<<< HEAD
                RaiseCommandStates();
=======
>>>>>>> main
            }
        }
    }

    public string ElapsedDisplay => $"{Elapsed:hh\\:mm\\:ss}";

    public decimal CurrentCharge
    {
        get => _currentCharge;
        private set => SetProperty(ref _currentCharge, value);
    }

<<<<<<< HEAD
    public string CustomerName
    {
        get => _customerName;
        set => SetProperty(ref _customerName, value);
    }

    public Brush StatusBrush
    {
        get
        {
            var brush = IsActive
                ? Application.Current?.FindResource("ActiveGreenBrush")
                : Application.Current?.FindResource("StoppedRedBrush");
            return brush as Brush ?? Brushes.Gray;
        }
    }
=======
    public Brush StatusBrush => IsActive ? Brushes.LimeGreen : Brushes.IndianRed;
>>>>>>> main

    public void Tick(TimeSpan delta)
    {
        if (!IsActive || IsPaused)
        {
            return;
        }

        Elapsed += delta;
        CurrentCharge = (decimal)Elapsed.TotalHours * RatePerHour;
    }

    private void StartSession()
    {
        IsActive = true;
        IsPaused = false;
<<<<<<< HEAD
        SessionStarted?.Invoke(this, new SessionEventArgs(Name, CustomerName));
=======
>>>>>>> main
    }

    private void PauseSession()
    {
        IsPaused = true;
    }

    private void StopSession()
    {
        IsActive = false;
        IsPaused = false;
<<<<<<< HEAD
        SessionStopped?.Invoke(this, new SessionEventArgs(Name, CustomerName, CurrentCharge, _sessionId));
        _sessionId = null;
    }

    private void ResetSession()
    {
=======
>>>>>>> main
        Elapsed = TimeSpan.Zero;
        CurrentCharge = 0m;
    }

<<<<<<< HEAD
    public void AttachSession(int sessionId)
    {
        _sessionId = sessionId;
    }

=======
>>>>>>> main
    private void RaiseCommandStates()
    {
        (StartCommand as RelayCommand)?.RaiseCanExecuteChanged();
        (PauseCommand as RelayCommand)?.RaiseCanExecuteChanged();
        (StopCommand as RelayCommand)?.RaiseCanExecuteChanged();
<<<<<<< HEAD
        (ResetCommand as RelayCommand)?.RaiseCanExecuteChanged();
    }
}

public record SessionEventArgs(string StationName, string CustomerName, decimal? Cost = null, int? SessionId = null);
=======
    }
}
>>>>>>> main

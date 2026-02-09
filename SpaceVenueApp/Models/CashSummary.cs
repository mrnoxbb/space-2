using SpaceVenueApp.ViewModels;

namespace SpaceVenueApp.Models;

public class CashSummary : ObservableObject
{
    private decimal _openingBalance;
    private decimal _totalDeposits;
    private decimal _totalWithdrawals;

    public decimal OpeningBalance
    {
        get => _openingBalance;
        set
        {
            if (SetProperty(ref _openingBalance, value))
            {
                RaisePropertyChanged(nameof(ExpectedClosing));
            }
        }
    }

    public decimal TotalDeposits
    {
        get => _totalDeposits;
        set
        {
            if (SetProperty(ref _totalDeposits, value))
            {
                RaisePropertyChanged(nameof(ExpectedClosing));
            }
        }
    }

    public decimal TotalWithdrawals
    {
        get => _totalWithdrawals;
        set
        {
            if (SetProperty(ref _totalWithdrawals, value))
            {
                RaisePropertyChanged(nameof(ExpectedClosing));
            }
        }
    }

    public decimal ExpectedClosing => OpeningBalance + TotalDeposits - TotalWithdrawals;
}

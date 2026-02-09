namespace SpaceVenueApp.Models;

public class CashSummary
{
    public decimal OpeningBalance { get; set; }
    public decimal TotalDeposits { get; set; }
    public decimal TotalWithdrawals { get; set; }
    public decimal ExpectedClosing => OpeningBalance + TotalDeposits - TotalWithdrawals;
}

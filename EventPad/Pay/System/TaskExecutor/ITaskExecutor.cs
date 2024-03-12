namespace EventPad.Pay;

public interface ITaskExecutor
{
    void CreateEventAccount();
    void CreateUserAccount();

    void DeleteEventAccount();
    void DeleteUserAccount();

    void CreatePurchase();
    void CreateRefund();
    void CreateCashout();
}

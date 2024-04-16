using EventPad.Pay.Context.Entities;

namespace EventPad.Pay.Context.Seeder;


public class DemoHelper
{
    public IEnumerable<UserAccount> GetUserAccounts = new List<UserAccount>
    {
        new UserAccount()
        {
            Id = 1,
            Uid = Guid.Parse("9766f76a-fdb0-4b21-bd03-625710a3f1f9"),
            AccountNumber = "1234567891012146",
            Balance = 1000
        },
        new UserAccount()
        {
            Id = 2,
            Uid = Guid.Parse("29252196-f44e-49e0-b69f-8a08ec21d27b"),
            AccountNumber = "4321567891021246",
            Balance = 2000        
        },
        new UserAccount()
        {
            Id = 3,
            Uid = Guid.Parse("43ba55b1-d0e9-44fc-aa7a-55263e8c721d"),
            AccountNumber = "4321659700021246",
            Balance = 5000
        }
    };
}

using EventPad.Pay.Context.Entities;

namespace EventPad.Pay.Context.Seeder;


public class DemoHelper
{
    private static Guid specificId1 = Guid.Parse("a09fdf29-ac10-48cb-b408-96b4ea8d1809");
    private static Guid specificId2 = Guid.Parse("4b7d1d7e-75a8-4761-98e7-d859c97e4be6");
    private static Guid specificId3 = Guid.Parse("cc15c23a-f10c-44d5-9224-55085243075b");
    private static Guid specificId4 = Guid.Parse("756009f5-d38d-4e71-951d-2eaa5c885c8a");
    private static Guid specificId5 = Guid.Parse("70cc5113-2558-4c2d-a38a-fb69e84edaff");
    private static Guid specificId6 = Guid.Parse("841ca45e-ac56-4cc2-bb82-c6983263dced");

    public IEnumerable<UserAccount> GetUserAccounts = new List<UserAccount>
    {
        new UserAccount()
        {
            Uid = Guid.Parse("142c4915-c4b3-4254-b0b2-8f0c34960c6e"),
            AccountNumber = "1234567891012146",
            Balance = 1000
        },
        new UserAccount()
        {
            Uid = Guid.Parse("29252196-f44e-49e0-b69f-8a08ec21d27b"),
            AccountNumber = "4321567891021246",
            Balance = 2000        
        },
        new UserAccount()
        {
            Uid = Guid.Parse("43ba55b1-d0e9-44fc-aa7a-55263e8c721d"),
            AccountNumber = "4321659700021246",
            Balance = 5000
        }
    };

    public IEnumerable<EventAccount> GetEventAccounts = new List<EventAccount>
    {
        new EventAccount()
        {
            Uid = specificId1,
            AccountNumber = "1234567891012146",
            Balance = 0
        },
        new EventAccount()
        {
            Uid = specificId2,
            AccountNumber = "1234567892212146",
            Balance = 0
        },
        new EventAccount()
        {
            Uid = specificId3,
            AccountNumber = "3214567891012146",
            Balance = 0
        },
        new EventAccount()
        {
            Uid = specificId4,
            AccountNumber = "1234569871012146",
            Balance = 0
        },
        new EventAccount()
        {
            Uid = specificId5,
            AccountNumber = "5432167891012146",
            Balance = 0
        },
        new EventAccount()
        {
            Uid = specificId6,
            AccountNumber = "9876543211012146",
            Balance = 0
        },
    };
}

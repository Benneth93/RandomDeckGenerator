namespace RandomDeckGenerator.Models;

public class User
{
    public string Username { get; set; }
    public string Password { get; set; }
    public List<string> StoredList { get; set; }

    public List<UserStoredList> UserStoredLists { get; set; }
    
    public void AddTestData()
    {
        UserStoredLists = new();
        var list = new UserStoredList()
        {
            Name = "TestList",
            List =
            {
                "This",
                "Is",
                "A",
                "List",
                "Of",
                "Things"
            }
        };
        var list2 = new UserStoredList()
        {
            Name = "TestListTwo",
            List =
            {
                "This2",
                "222",
                "22222",
                "222222",
                "1213425",
                "Things"
            }
        };
                
                
        UserStoredLists.Add(list);
        UserStoredLists.Add(list2);
    }
}
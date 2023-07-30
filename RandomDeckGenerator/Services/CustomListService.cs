using RandomDeckGenerator.Models;

namespace RandomDeckGenerator.Services;

public static class CustomListService
{
    public static List<UserStoredList> GetUserStoredLists(string userName)
    {
        return new List<UserStoredList>();
    }
}
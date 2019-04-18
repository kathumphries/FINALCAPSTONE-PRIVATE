using Capstone.Models;

namespace Capstone.DAL.Interfaces
{
    internal interface IUserEventSqlDal
    {
        bool AddMyEvent(int userID, Event eventItem);
        bool RemoveMyEvent(int userID, int eventID);

    }
}
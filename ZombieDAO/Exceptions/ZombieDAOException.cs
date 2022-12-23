namespace ZombieDAO.Exceptions; 

public class ZombieDAOException : Exception {
    public string? SendMessage { get; }
    
    public ZombieDAOException() { }
    public ZombieDAOException(string message) {
        SendMessage = message;
    }
}

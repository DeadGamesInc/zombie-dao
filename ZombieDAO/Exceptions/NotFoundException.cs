namespace ZombieDAO.Exceptions;

public sealed class NotFoundException : ZombieDAOException {
    public NotFoundException() { }
    public NotFoundException(string message) : base(message) { }
}


using MobilePractice.Data;

namespace MobilePractice.Services;
public class UserService {
    private readonly PracticeContext _context;

    public UserService(PracticeContext context) {
        _context = context;
    }
}
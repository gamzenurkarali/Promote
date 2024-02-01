using Promote.website.Models;

namespace Promote.website.Services
{
    public class LayoutService
    {
        private readonly Context _context;

        public LayoutService(Context context)
        {
            _context = context;
        }
        public Layout GetLayout()
        {
            return _context.layouts.FirstOrDefault();
        }

        public List<Sublink> GetSublinks()
        {
            return _context.sublinks.ToList();
        }
    }
}

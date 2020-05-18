namespace ChessWebApplication.Controllers.Archive
{
    using ChessWebApplication.Common;
    using Microsoft.AspNetCore.Mvc;

    public class ArchiveController : MediatorController
    {
        public ArchiveController()
        {
        }

        public IActionResult Games()
        {
            return this.View();
        }
    }
}

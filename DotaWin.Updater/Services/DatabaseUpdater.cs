using DotaWin.Data;
using Microsoft.EntityFrameworkCore;

namespace DotaWin.Updater.Services;

internal class DatabaseUpdater
{
    private readonly DotaWinDbContext _db;
    public DatabaseUpdater(DbContextOptions options)
    {
        _db = new DotaWinDbContext(options);
    }

    public async Task UpdateHeroItems()
    {
        // move the update logic here
    }
}

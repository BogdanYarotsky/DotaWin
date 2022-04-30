using DotaWin.Data;
using Microsoft.EntityFrameworkCore;

namespace DotaWin.Updater.Services;

internal class DatabaseUpdater
{
    public enum UpdateResult
    {
        Success, UpToDate, Fail
    }

    private readonly DotaWinDbContext _db;
    public DatabaseUpdater(DbContextOptions options)
    {
        _db = new DotaWinDbContext(options);
    }

    public async Task<UpdateResult> RunDailyUpdate()
    {
        var lastUpdate = await _db.DailyUpdates.OrderBy(upd => upd.Date).LastOrDefaultAsync();
        if (lastUpdate != null && lastUpdate.Date.Date == DateTime.Now.Date)
        {
            return UpdateResult.UpToDate;
        }

        return UpdateResult.Fail;
    }

    private void UpdateDatabase()
    {
        throw new NotImplementedException();
    }
}

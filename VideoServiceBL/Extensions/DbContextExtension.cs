using System.Linq;
using Microsoft.EntityFrameworkCore;
using VideoServiceDAL.Interfaces;

namespace VideoServiceBL.Extensions
{
    public static class DbContextExtension
    {
        public static void DetachLocal<T>(this DbContext context, T t, long? entryId = null)
            where T : class, IIdentifier
        {
            if (!entryId.HasValue)
            {
                context.Entry(t).State = EntityState.Modified;
                return;
            }

            var local = context.Set<T>()
                .Local
                .FirstOrDefault(entry => entry.Id.Equals(entryId));
            if (local != null)
            {
                context.Entry(local).State = EntityState.Detached;
            }
            context.Entry(t).State = EntityState.Modified;
        }
    }

}
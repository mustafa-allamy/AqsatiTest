using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Persistence
{
    public class DbContextDateTimeConverter : ValueConverter<DateTime, DateTime>
    {
        public DbContextDateTimeConverter()
            : base(
                // the expression to convert objects when passing data to the store
                v => ConvertTo(v),
                // the expression to convert objects when reading data from the store
                v => ConvertFrom(v))
        { }

        private static DateTime ConvertTo(DateTime value)
        {
            // we need to store utc in our db
            value = value.Kind switch
            {
                DateTimeKind.Local => value.ToUniversalTime(),
                DateTimeKind.Unspecified => throw new Exception("Date time kind is unspecified."),
                DateTimeKind.Utc => value,
                _ => value,
            };
            return value;
        }

        private static DateTime ConvertFrom(DateTime value)
        {
            // we kUtcNow, that we store utc in our db
            value = value.Kind == DateTimeKind.Unspecified ?
                DateTime.SpecifyKind(value, DateTimeKind.Utc) :
                value;
            return value;
        }
    }
}
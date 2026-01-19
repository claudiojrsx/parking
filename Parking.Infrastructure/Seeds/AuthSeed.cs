namespace Parking.Infrastructure.Seeds
{
    public static class AuthSeed
    {
        public static readonly Guid AdminRoleId =
            Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");

        public static readonly Guid AdminUserId =
            Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");

        public const string AdminPasswordHash =
            "$2b$10$pvVtvha0LggnboHgBUA6J.kWPgprV2QsIBeXuhSI0hplO5mSeWJGa";
    }
}

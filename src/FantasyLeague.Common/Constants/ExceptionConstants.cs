namespace FantasyLeague.Common.Constants
{
    public static class ExceptionConstants
    {
        public const string CreateRoleException = "Create role was unsuccessful.";

        public const string CreateUserException = "Create user was unsuccessful.";

        public const string AddUserToRoleException = "Add user to role was unsuccessful.";

        public const string NotFoundException = "{0} was not found.";

        public const string InvalidEnumException = "Invalid type for {0}.";

        public const string FailedUploadException = "The file upload failed.";

        public const string AlreadyGeneratedException = "Player Scores for the matchday({0}) are already generated.";

        public const string AlreadyTakenClubNameException = "Club name '{0}' is already taken.";

        public const string UnknownValueException = "{0} is not recognised as valid {1}.";

    }
}

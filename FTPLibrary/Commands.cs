namespace FTPLibrary
{
    public static class Commands
    {
        public const byte Authenticate = 255;
        public const byte Upload = 0;
        public const byte Download = 1;
        public const byte Login = 2;
        public const byte Logout = 3;
        public const byte ErrorPasswrong = 5;
        public const byte ErrorUserDoesNotExist = 6;
        public const byte ErrorUserExsists = 7;
        public const byte ErrorUserNotLoggedIn = 8;
        public const byte ErrorRegistrationDeatilsWrong = 9;
        public const byte ErrorUnknownCommand = 10;
        public const byte Ready = 11;
        public const byte ErrorUnknownRecipient = 12;
        public const byte SendingRootNode = 13;
        public const byte Setup = 14;
        public const byte Ping = 15;
        public const byte Correct = 50;
        public const byte ErrorIncorrect = 70;
        public const byte File = 90;
        public const byte Files = 91;
        public const byte Directory = 92;
        public const byte ErrorUserAlreadyLoggedIn = 16;
        public const byte SizeTooLarge = 34;
        public const byte RequestRootNode = 13;
        public const byte Shutdown = 45;
        public const byte All = 76;
        public const byte OneAtATime = 77;
        public const byte Finished = 54;
        public static readonly byte[] FinishedArray = { 54 };


    }
}

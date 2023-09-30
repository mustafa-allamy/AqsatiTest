namespace Infrastructure.Authentication
{
    public class ControllerPermissionDto
    {
        public string Name => ClassName + "_" + MethodName + "_" + MethodType;

        public string PolicyName { get; set; }
        public string GivenName { get; set; }
        public string ClassName { get; set; }
        public string MethodName { get; set; }
        public string MethodType { get; set; }

    }
}
using System;


namespace R5T.S0030
{
    public static class Reasons
    {
        public static string ClassInServicesDefinitionsDirectory => "Class is misplaced in /Services/Definitions directory.";
        public static string ClassInServicesInterfacesDirectory => "Class is misplaced in /Services/Interfaces directory.";
        public static string LacksMarkerAttribute => "Lacks marker attribute.";
        public static string LacksMarkerInterface => "Lacks marker interface.";
        public static string InOldServicesClassesDirectory => "In old /Services/Classes directory.";
        public static string InOldServicesInterfacesDirectory => "In old /Services/Interfaces directory.";
        public static string InterfaceInServicesImplementationsDirectory => "Interface is misplaced in /Services/Implementations directory.";
        public static string InterfaceInServicesClassesDirectory => "Interface is misplaced in /Services/Classes directory.";
        public static string WrongMarkerAttribute => "The marker attribute has the right type name, but the wrong namespaced type name.";
    }
}

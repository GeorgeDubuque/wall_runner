using System.Collections;
using System.Collections.Generic;

public static class PlayerData
{
    public static int currLevelInd = 0;
    static Level blue = new Level("Blue Level", "");
    static Level green = new Level("Green Level", "");
    public static Level[] levels = { blue, green};
    public static Dictionary<string, Level> levelDict = new Dictionary<string, Level>() {
        ["Blue Level"]=blue,
        ["Green Level"]=green
    };
}

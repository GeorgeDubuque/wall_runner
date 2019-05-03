using System.Collections;
using System.Collections.Generic;

public static class PlayerData
{
    public static int currLevelInd = 0;
    static Level blue = new Level("Blue Level", "", 0.0f);
    static Level green = new Level("Green Level", "", 0.0f);
    static Level orange = new Level("Orange Level", "", 0.0f);
    public static Level[] levels = { blue, green, orange};
    public static Dictionary<string, Level> levelDict = new Dictionary<string, Level>() {
        ["Blue Level"]=blue,
        ["Green Level"]=green,
        ["Orange Level"]=orange
    };
}

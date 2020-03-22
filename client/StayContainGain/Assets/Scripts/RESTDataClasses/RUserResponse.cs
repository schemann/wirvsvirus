using System;
using System.Collections.Generic;

[Serializable]
public class RUserRespnse
{
    public RHomeBase homeBase;
    public DateTime homeBaseSetDate;
    public int currentLevel;
    public int currentXP;
    public List<string> friendlist;
    public List<object> equimentList;
    public List<object> currentEquimentList;
    public string _id;
    public string username;
    public DateTime createdDate;
}

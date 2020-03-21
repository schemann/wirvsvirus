using System;
using System.Collections.Generic;

public class RAuthResponse
{
    public RHomeBase homeBase { get; set; }
    public object homeBaseSetDate { get; set; }
    public int currentLevel { get; set; }
    public int currentXP { get; set; }
    public List<object> friendList { get; set; }
    public List<object> equimentList { get; set; }
    public List<object> currentEquimentList { get; set; }
    public string _id { get; set; }
    public string username { get; set; }
    public DateTime createdDate { get; set; }
    public int __v { get; set; }
    public string token { get; set; }
}
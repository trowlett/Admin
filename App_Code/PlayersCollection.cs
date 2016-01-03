using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PlayersCollection
/// </summary>
public class PlayersCollection
{
    public Collection<Players> roster = new Collection<Players>();
    public Collection<Players> Roster
    {
        get
        {
            return this.roster;
        }
    }


	public PlayersCollection()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}
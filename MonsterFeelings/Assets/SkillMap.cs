using UnityEngine;
using System;
using System.Collections.Generic;

public class SkillMap
{
		private List<Skill> skills;

		// Given a string re
		public SkillMap (string acquiredSkills)
		{
				// Generate a list of all of the skills.
				skills = new List<Skill> ()
				{
					new Slash()
					//new PowerAttack(),
					//new DefensiveStance()
				};

				string[] acquired = acquiredSkills.Split (',');
				
				for (int i = 0; i < acquired.Length; i++) {
						int skillNum = Convert.ToInt32 (acquired [i]);
						skills [skillNum].isAcquired = true;
				}
		}

		// Returns all skills that the character has.
		public List<Skill> getAcquiredSkills ()
		{
				List<Skill> acquired = new List<Skill> ();

				foreach (Skill s in skills) {
						if (s.isAcquired) {
								acquired.Add (s);
						}
				}

				return acquired;
		}
}



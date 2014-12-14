using UnityEngine;
using System;
using System.Collections.Generic;

public class SkillMap
{
		private List<Skill> skills;

		// Given a string re
		public SkillMap (string acquiredSkills, Character user)
		{
				// Generate a list of all of the skills.
				skills = new List<Skill> ()
				{
					new Slash(user),
					new PowerAttack(user),
					new DefensiveStance(user),
					new Assault(user),
					new RoguesMark(user),
					new Stealth(user),
					new MagicMissile(user),
					new ArcaneBrilliance(user),
					new ManaShield(user),
				};

				string[] acquired = acquiredSkills.Split (',');
				
				foreach (string s in acquired) {
						char[] currSkill = s.ToCharArray ();
						int skillNum = int.Parse (currSkill [0].ToString ());
						skills [skillNum].isAcquired = true;

						foreach (char c in currSkill) {
								if (c == '+') {
										skills [skillNum].upgrade ();
								}
						}
				}
		}

		// Returns all skills that the character has.
		// Call whenever the character might get a new skill.
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
		
		public void acquireSkill (int i)
		{
				skills [i] .isAcquired = true;
		}
}



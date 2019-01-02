using FantasyLeague.Common.Constants;
using FantasyLeague.Models.Abstract;
using FantasyLeague.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FantasyLeague.Models
{
    public class Score : BaseEntity
    {
        [Required]
        public Guid PlayerId { get; set; }
        public virtual Player Player { get; set; }

        [Required]
        public Guid FixtureId { get; set; }
        public virtual Fixture Fixture { get; set; }

        [Required]
        [Range(minimum: 0, maximum: int.MaxValue)]
        public int Goals { get; set; }

        [Required]
        [Range(minimum: 0, maximum: int.MaxValue)]
        public int GoalsConceded { get; set; }

        [Required]
        [Range(minimum: 0, maximum: int.MaxValue)]
        public int Shots { get; set; }

        [Required]
        [Range(minimum: 0, maximum: int.MaxValue)]
        public int Assists { get; set; }

        [Required]
        [Range(minimum: 0, maximum: int.MaxValue)]
        public int Tackles { get; set; }

        [Required]
        [Range(minimum: 0, maximum: ScoreConstants.MaxYellowCards)]
        public int YellowCards { get; set; }

        [Required]
        [Range(minimum: 0, maximum: ScoreConstants.MaxRedsCards)]
        public int RedCards { get; set; }

        [Required]
        public bool CleanSheet { get; set; }

        public bool? IsWiner { get; set; }

        [Required]
        [Range(minimum: 0, maximum: ScoreConstants.MaxPlayedMinutesValue)]
        public int PlayedMinutes { get; set; }
        
        public int GetScore()
        {
            var position = this.Player.Position;

            int result = 0;

            //Attacking Stats
            result += this.Goals * (int)position;
            result += this.Assists * ((int)position - ScoreConstants.AssistParam);
            result += this.Shots * ((int)position - ScoreConstants.ShotParam);

            //Defensive stats
            result += this.Tackles;

            if ((int)position > (int)PlayerPosition.Midfielder)
            {
                if (this.CleanSheet)
                {
                    result += ScoreConstants.CleanSheetParam;
                }
                else
                {
                    result -= this.GoalsConceded;
                }
                result += (int)position;
            }

            //Other stats
            result += this.PlayedMinutes / ScoreConstants.PlayedMinutesDivider;
            result -= this.YellowCards * ScoreConstants.CardParam;
            result -= this.RedCards * ScoreConstants.CardParam * ScoreConstants.CardParam;

            if (IsWiner.HasValue)
            {
                result += this.IsWiner.Value ? 1 : -2;
            }

            return result;
        }
        
    }
}

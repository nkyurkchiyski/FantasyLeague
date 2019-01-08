using FantasyLeague.Common.Constants;
using FantasyLeague.Models.Abstract;
using FantasyLeague.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;

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
            int position = (int)this.Player.Position;

            int result = 0;

            //Primary Stats
            result += this.Goals * (ScoreConstants.GoalParam + ScoreConstants.TertiaryParam);
            result += this.Assists * (position - ScoreConstants.TertiaryParam);
            result += this.Shots / ScoreConstants.SecondaryParam;

            //Defensive stats
            result += this.Tackles / ScoreConstants.SecondaryParam;

            if (this.CleanSheet)
            {
                result += ScoreConstants.PrimaryParam;
            }
            else
            {
                result -= this.GoalsConceded;
            }

            if (position == (int)PlayerPosition.Goalkeeper)
            {
                result += (position - ScoreConstants.PrimaryParam);
            }

            //Other stats
            result += this.PlayedMinutes / ScoreConstants.PlayedMinutesDivider;
            result -= this.YellowCards * ScoreConstants.CardParam;
            result -= this.RedCards * ScoreConstants.CardParam * ScoreConstants.CardParam;

            if (IsWiner.HasValue)
            {
                result += this.IsWiner.Value ? ScoreConstants.OutcomeBonus :
                    -(ScoreConstants.OutcomeBonus);
            }

            return result;
        }

    }
}

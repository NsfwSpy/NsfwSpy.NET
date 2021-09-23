using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsfwSpyNS
{
    /// <summary>
    /// The result from classifying an image.
    /// </summary>
    public class NsfwSpyResult
    {
        /// <summary>
        /// The hentai probability score between 0 and 1.
        /// </summary>
        public float Hentai { get; }

        /// <summary>
        /// The neutral probability score between 0 and 1.
        /// </summary>
        public float Neutral { get; }

        /// <summary>
        /// The pornography probability score between 0 and 1.
        /// </summary>
        public float Pornography { get; }

        /// <summary>
        /// The sexy probability score between 0 and 1.
        /// </summary>
        public float Sexy { get; }

        /// <summary>
        /// The most likely predicted value.
        /// </summary>
        public string PredictedLabel { get; }

        /// <summary>
        /// Whether the image is likely to be explicit. True if the sum of nsfw, hentai and sexy is equal to or above 0.5.
        /// </summary>
        public bool IsNsfw => Neutral < 0.5;

        public NsfwSpyResult()
        {

        }

        internal NsfwSpyResult(ModelOutput modelOutput)
        {
            Hentai = modelOutput.Score[(int)EClassificationType.Hentai];
            Neutral = modelOutput.Score[(int)EClassificationType.Neutral];
            Pornography = modelOutput.Score[(int)EClassificationType.Pornography];
            Sexy = modelOutput.Score[(int)EClassificationType.Sexy];
            PredictedLabel = modelOutput.PredictedLabel;
        }

        /// <summary>
        /// Get the 5 classification types as a dictionary ordered by their score.
        /// </summary>
        /// <returns>Dictionary of the prediction scores.</returns>
        public Dictionary<string, float> ToDictionary()
        {
            var dictionary = new Dictionary<string, float>
            {
                { "Hentai", Hentai },
                { "Neutral", Neutral },
                { "Pornography", Pornography },
                { "Sexy", Sexy }
            };

            return dictionary.OrderByDescending(p => p.Value).ToDictionary(x => x.Key, x => x.Value); ;
        }
    }
}

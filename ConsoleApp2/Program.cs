namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> wordstoFind = new List<string>();
            wordstoFind.Add("chill");
            wordstoFind.Add("cold");
            wordstoFind.Add("wind");
            wordstoFind.Add("waca");
            List<string> matrixSource = new List<string>();
            matrixSource.Add("chillhood");
            matrixSource.Add("achillhoo");
            matrixSource.Add("acoldest3");
            matrixSource.Add("wwwww0ind");
            matrixSource.Add("aiwindatr");
            matrixSource.Add("anaaasatr");
            matrixSource.Add("adcoldatr");
            matrixSource.Add("aaaaasatr");
            matrixSource.Add("achillatr");
            IEnumerable<string> words2 = matrixSource.ToArray();
            WordFinder wordFinder = new(words2);
            var result1 = wordFinder.Find(wordstoFind);
            foreach(var word in result1) 
            {
                Console.WriteLine(word);
            }
            Console.ReadLine();
        }

        public class WordFinder
        {
            private readonly char[][] _matrix;
            private readonly int _size;

            public WordFinder(IEnumerable<string> matrix)
            {
                _size = matrix.First().Length;
                _matrix = matrix.Select(row => row.ToCharArray()).ToArray();
            }

            public IEnumerable<string> Find(IEnumerable<string> wordstream)
            {
                var words = new HashSet<string>();
                var foundWords = new HashSet<string>();

                foreach (var word in wordstream)
                {
                    if (words.Contains(word))
                    {
                        continue;
                    }

                    if (FindHorizontal(word, foundWords) || FindVertical(word, foundWords))
                    {
                        words.Add(word);
                    }
                }

                return words.GroupBy(w => w)
                    .OrderByDescending(g => g.Count())
                    .Take(10)
                    .Select(g => g.Key);
            }

            private bool FindHorizontal(string word, HashSet<string> foundWords)
            {
                for (var i = 0; i < _size; i++)
                {
                    for (var j = 0; j <= _size - word.Length; j++)
                    {
                        var candidate = new string(_matrix[i], j, word.Length);
                        if (candidate == word)
                        {
                            foundWords.Add(word);
                            return true;
                        }
                    }
                }

                return false;
            }

            private bool FindVertical(string word, HashSet<string> foundWords)
            {
                for (var i = 0; i <= _size - word.Length; i++)
                {
                    for (var j = 0; j < _size; j++)
                    {
                        var candidate = new string(Enumerable.Range(i, word.Length).Select(x => _matrix[x][j]).ToArray());
                        if (candidate == word)
                        {
                            foundWords.Add(word);
                            return true;
                        }
                    }
                }

                return false;
            }
        }
    }
}
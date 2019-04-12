using System.Collections.Generic;
using System.Linq;

namespace TestNinja.Mocking
{
    public class VideoRepository : IVideoRepository
    {
        public IEnumerable<Video> GetUnprocessedVideos()
        {
            using (var context = new VideoContext())
            {
                return (from video in context.Videos
                        where !video.IsProcessed
                        select video).ToList();
            }
        }
    }

    public interface IVideoRepository
    {
        IEnumerable<Video> GetUnprocessedVideos();
    }
}

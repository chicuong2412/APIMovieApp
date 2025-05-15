using API.DTOs.Video;
using API.Models;

namespace API.Mappers
{
    public static class VideoMappers
    {

        public static Video MapToVideo(this VideoCreationRequest videoCreationRequest)
        {
            return new Video
            {
                Code = videoCreationRequest.Code,
                Url = videoCreationRequest.Url
            };
        }

        public static Video MapToVideo(this VideoUpdationRequest videoUpdationRequest)
        {
            return new Video
            {
                Code = videoUpdationRequest.Code,
                Url = videoUpdationRequest.Url
            };
        }

    }
}

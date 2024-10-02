using Dapper;
using VideoStreaming.Entities;
using VideoStreaming.Repositories.RepositoryInterfaces;
using System.Data.SqlClient;

namespace VideoStreaming.Repositories.RepositoryImplementations
{
    public class VideoRepository : IVideoRepository
    {
        private readonly IConfiguration configuration;
        public VideoRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<int> AddAsync(Video video)
        {
            var sql = "INSERT INTO Videos (FileName, UploadDate) VALUES (@FileName,@UploadDate)";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, video);
                return result;
            }
        }

        public async Task<int> UpdateAsync(Video video)
        {

            var sql = "UPDATE Videos SET FileName=@FileName, UploadDate=@UploadDate WHERE VideoId=@VideoId";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, video);
                return result;
            }
        }
        public async Task<int> DeleteAsync(int id)
        {
            var sql = "DELETE FROM Videos WHERE VideoId=@VideoId";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new { BannerId = id });
                return result;
            }
        }
        public async Task<Video> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM Videos WHERE VideoId=@VideoId";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<Video>(sql, new { VideoId = id });
                return result;
            }
        }
        public async Task<IReadOnlyList<Video>> GetAllAsync()
        {
            var sql = "SELECT * FROM Videos";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Video>(sql);
                return result.ToList();
            }
        }
    }
}

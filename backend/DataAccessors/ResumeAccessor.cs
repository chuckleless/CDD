using System.Threading.Tasks;
using System.Text;
using SyaApi.Plugins;
using SyaApi.Entities;

namespace SyaApi.DataAccessors
{
    public static class ResumeAccessor
    {
        public static async Task<ResumeEntity> Find(int id)
        {
            var query = "SELECT resume_id,user_id,age,name,city,education,community,project,academic,skill,introduction FROM resume WHERE user_id=@id";

            using var connection = DatabaseConnector.Connect();
            await connection.OpenAsync();
            using var command = connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@id",id);

            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new ResumeEntity
                {
                    resume_id=reader.GetInt32("resume_id"),
                    student_id=reader.GetInt32("user_id"),
                    age=reader.GetInt32("age"),
                    student_name=reader.GetString("name"),
                    city=reader.GetString("city"),
                    education=reader.GetString("education"),
                    community=reader.GetString("community"),
                    project=reader.GetString("project"),
                    academic=reader.GetString("academic"),
                    skill=reader.GetString("skill"),
                    introduction=reader.GetString("introduction")               
                };
            }
            return null;
        }

        public static async Task<int> Change(ResumeEntity resume)
        {
            var query = "UPDATE resume SET age=@age,name=@name,city=@city,education=@education,community=@community,project=@project,academic=@academic,skill=@skill,introduction=@introduction where user_id=@id";

            using var connection = DatabaseConnector.Connect();
            await connection.OpenAsync();
            using var command = connection.CreateCommand();
            command.CommandText = query;

            command.Parameters.AddWithValue("@id", resume.student_id);
            command.Parameters.AddWithValue("@age", resume.age);
            command.Parameters.AddWithValue("@name", resume.student_name);
            command.Parameters.AddWithValue("@city", resume.city);
            command.Parameters.AddWithValue("@education", resume.education);
            command.Parameters.AddWithValue("@community", resume.community);
            command.Parameters.AddWithValue("@project", resume.project);
            command.Parameters.AddWithValue("@academic", resume.academic);
            command.Parameters.AddWithValue("@skill", resume.skill);
            command.Parameters.AddWithValue("@introduction", resume.introduction);

            var row = await command.ExecuteNonQueryAsync();
            if(row>0)
            {
                return resume.student_id;
            }
            return 0;
        }
        


        public static async Task<int> Create(ResumeEntity resume)
        {
            var query = "INSERT INTO resume(user_id) VALUES(@id)";

            using var connection = DatabaseConnector.Connect();
            await connection.OpenAsync();
            using var command = connection.CreateCommand();
            command.CommandText = query;

            command.Parameters.AddWithValue("@id", resume.student_id);
           

            await command.ExecuteNonQueryAsync();
            return (int)command.LastInsertedId;
        }

        public static async Task<int> Delete(int id)
        {
            var query = "DELETE FROM resume where user_id=@id";

            using var connection = DatabaseConnector.Connect();
            await connection.OpenAsync();
            using var command = connection.CreateCommand();
            command.CommandText = query;

            command.Parameters.AddWithValue("@id", id);
            var row = await command.ExecuteNonQueryAsync();
            if(row>0)
            {
                return id;
            }
            return 0;
        }

        
        ///<summery>
        /// 根据student_id查找resume_id
        /// chuckle 8.26
        ///</summery>
        public static async Task<int> GetResume(int id)
        {
            
            var query = "SELECT resume_id FROM resume WHERE user_id=@id";

            using var connection = DatabaseConnector.Connect();
            await connection.OpenAsync();
            using var command = connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@id",id);

            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return reader.GetInt32("resume_id");
            }
            return -1; // work not exists
        }


        public static async Task<ResumeEntity> FindInfo(int id)
        {
            var query = "SELECT resume_id,user_id,age,name,city,education,community,project,academic,skill,introduction FROM resume WHERE resume_id=@id";

            using var connection = DatabaseConnector.Connect();
            await connection.OpenAsync();
            using var command = connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@id",id);

            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new ResumeEntity
                {
                    resume_id=reader.GetInt32("resume_id"),
                    student_id=reader.GetInt32("user_id"),
                    age=reader.GetInt32("age"),
                    student_name=reader.GetString("name"),
                    city=reader.GetString("city"),
                    education=reader.GetString("education"),
                    community=reader.GetString("community"),
                    project=reader.GetString("project"),
                    academic=reader.GetString("academic"),
                    skill=reader.GetString("skill"),
                    introduction=reader.GetString("introduction")               
                };
            }
            return null;
        }
    }

}
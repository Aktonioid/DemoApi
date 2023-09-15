using DemoApi.Dto;
using DemoApi.Repositories.DB;
using DemoApi.Settings;
using dotenv.net;
using MongoDB.Bson;
using MongoDB.Driver;
using models;
using System.Text;

namespace DemoApi.Repositories
{
    public class MaterialsRepo : IMaterialsRepo
    {
        private static readonly string UrlPath = EnvDict()["FilesPath"];
        private Mongo client = new("Materials");
        private readonly string MGroup = "Materials"; //Название таблицы для MaterilasGroup

        private static IDictionary<string, string> EnvDict()
        {
            return DotEnv.Read(options: new DotEnvOptions(envFilePaths: new[] { "settings.env" }));
        }

        public async Task<MaterialsGroupDto> CreateMaterialsGroupAsync(MaterialsGroupDto filedto)
        {
            MaterialsGroup file = new()
            {
                Id = Guid.NewGuid(),
                Title = filedto.Title,
                Files = new List<MaterialsFile>(),
            };
           
            await client.CreateAsync<MaterialsGroup>(file);
           
            return file.AsDto();
        }

        

        public async Task<MaterialsFileDto> CreateMAterialsFileAsyncT(Guid GroupId, IFormFile file, IWebHostEnvironment env)
        {

            string filename = file.FileName;

            string path = env.ContentRootPath + "files";

            if(!Directory.Exists(path))
            {
                
                Directory.CreateDirectory(path);
            }


            MaterialsFile MFile = new()
            {
                Id = Guid.NewGuid(),
                Title = filename,
                Url = path +"/"+ filename
            };

            using(FileStream stream = System.IO.File.Create(path+"/"+filename))
            {
                
                file.CopyTo(stream);
                stream.Flush();
            }

            var find = await client.FindByIdAsync<MaterialsGroup>(GroupId);
            var list = find.Files;

            list.Add(MFile);

            find = find with
            {
                Files = list
            };

            await client.UpdateAsync<MaterialsGroup>(find,GroupId);
            return MFile.AsDto();
        }

        public async Task DeleteMaterialsFileAsync(Guid Id, string name)
        {
            var path = UrlPath + name;
            var find = await client.FindByIdAsync<MaterialsGroup>(Id) ;
            var list = find.Files;
            
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Title == name)
                {
                    list.RemoveAt(i);
                    break;
                }
            }
            
            find = find with{ Files = list };
            
            await client.UpdateAsync<MaterialsGroup>(find, Id);
        }

        public async Task DeleteMaterialsGroupAsync(Guid Id)
        {
            await client.DeleteAsync<MaterialsGroup>(Id);
        }

        public async Task<IEnumerable<MaterialsGroupDto>> GetMaterialsGroupAsync()
        {
            List<MaterialsGroup> materials = await client.FindAsync<MaterialsGroup>();
            
            return materials.Select(materials => materials.AsDto());
        }

        public async Task<MaterialsGroupDto> GetMaterialsGroupAsyncById(Guid GroupId)
        {
            return (await client.FindByIdAsync<MaterialsGroup>(GroupId)).AsDto();
        }

        // public async Task<MaterialsFileDto> getMaterialsFileByName(Guid gropuId, string filename)
        // {
        //     Console.WriteLine("shit");
        //     var find = await client.FindByIdAsync<MaterialsGroup>(gropuId) ;
        //     var list = find.Files;
        //     MaterialsFileDto fileDto = new MaterialsFileDto();

        //     for(int i = 0; i < list.Count; i++)
        //     {
        //         if(list[i].Title == filename)
        //         {
        //             fileDto.Id = list[i].Id;
        //             fileDto.Title = list[i].Title;
        //             fileDto.Url = list[i].Url;
        //         }
        //     }

        //     return fileDto;
        // }
    }
}

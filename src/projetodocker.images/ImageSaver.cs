using System;
using System.IO;

namespace images
{
    public class ImageSaver
    {
        /// <summary>
        /// Salva uma imagem a partir de uma string Base64 em um caminho especificado.
        /// </summary>
        /// <param name="base64Image">String da imagem codificada em Base64.</param>
        /// <param name="filePath">Caminho completo onde a imagem será salva (incluindo o nome do arquivo e extensão).</param>
        /// <returns>Retorna true se a imagem for salva com sucesso, caso contrário, false.</returns>
        public bool SaveImageFromBase64(string base64Image, string idImage)
        {
            try
            {
                string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "images");

                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                string filePath = Path.Combine(folderPath, idImage+".jpg");

                // Remove o prefixo "data:image/...;base64," se estiver presente
                var base64Data = base64Image.Contains(",") ? base64Image.Split(',')[1] : base64Image;

                // Converte a string Base64 em um array de bytes
                byte[] imageBytes = Convert.FromBase64String(base64Data);

                // Salva o array de bytes como um arquivo de imagem no disco
                File.WriteAllBytes(filePath, imageBytes);
                return true;
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"Erro ao salvar a imagem: {ex.Message}");
                return false;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;

namespace VScodeIntelliSenseForPIC
{
    class DataSerializer<T>
    {
        // ジェネリック：<T> => データの型
        
        // フィールド
        private T ProcessingList { get; set; }

        // メソッド

        /// <summary>
        /// <T>型のデータをjson形式にシリアライズするメソッド
        /// </summary>
        /// <param name="SerializeData">T型のデータ</param>
        /// <returns></returns>
        public string SerializeToJson(T SerializeData)
        {
            // 出力するjson文字列のオプション指定
            var jsonoptions = new JsonSerializerOptions
            {
                // エンコードの設定
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                // Dictionaly用設定
                DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
                // インデントの設定
                WriteIndented = true
            };

            // json文字列を生成
            string jsonstring = JsonSerializer.Serialize(SerializeData, jsonoptions);

            // 文字列を返却
            return jsonstring;
        }

        /// <summary>
        /// json形式文字列をT型にデシリアライズするメソッド
        /// </summary>
        /// <param name="jsontext">json文字列</param>
        /// <returns>T型のデータ</returns>
        public T DeserializeFromJson(string jsontext)
        {
            // jsonのオプション設定
            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            // jsonデシリアライズ
            ProcessingList = JsonSerializer.Deserialize<T>(jsontext, options);

            // 返却
            return ProcessingList;
        }
    }
}

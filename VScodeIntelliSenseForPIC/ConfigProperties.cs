namespace VScodeIntelliSenseForPIC
{
    /// <summary>
    /// c_cpp_properties.jsonの設定内容クラス
    /// </summary>
    public class ConfigProperties
    {
        // configurations以下の設定内容
        public Configurations[] configurations { get; set; }
        // version
        public int version { get; set; }

        /// <summary>
        /// コンストラクタ(引数無し)
        /// </summary>
        public ConfigProperties() {}

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="data">configurations以下の設定オブジェクト</param>
        /// <param name="version">version</param>
        public ConfigProperties(Configurations[] data, int version)
        {
            this.configurations = data;
            this.version = version;
        }
    }

    /// <summary>
    /// configurations以下の設定内容クラス
    /// </summary>
    public class Configurations
    {
        // name:識別名
        public string name { get; set; }
        // includePath:インクルードパス
        public string[] includePath { get; set; }
        // defines:プリプロセッサ定義
        public string[] defines { get; set; }
        // intelliSenseMode:IntelliSenseモード
        public string intelliSenseMode { get; set; }
        // browse:IntelliSense用インクルードパス等の設定
        public Browse browse { get; set; }
        // cStandard:C言語標準バージョン
        public string cStandard { get; set; }
        // cppStandard:C++言語標準バージョン
        public string cppStandard { get; set; }

        /// <summary>
        /// コンストラクタ(引数無し)
        /// </summary>
        public Configurations() {}

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name">識別名</param>
        /// <param name="includePath">インクルードパス</param>
        /// <param name="defines">プリプロセッサ定義</param>
        /// <param name="intelliSenseMode">IntelliSenseモード</param>
        /// <param name="browse">IntelliSense用設定</param>
        /// <param name="cStandard">C言語標準バージョン</param>
        /// <param name="cppStandard">C++言語標準バージョン</param>
        public Configurations(
            string name, 
            string[] includePath,
            string[] defines,
            string intelliSenseMode,
            Browse browse,
            string cStandard,
            string cppStandard)
            {
                this.name = name;
                this.includePath = includePath;
                this.defines = defines;
                this.intelliSenseMode = intelliSenseMode;
                this.browse = browse;
                this.cStandard = cStandard;
                this.cppStandard = cppStandard;
            }
    }

    /// <summary>
    /// IntelliSense設定用クラス
    /// </summary>
    public class Browse
    {
        // ヘッダファイルパス
        public string[] path { get; set; }
        // データベースへのパス
        public string databaseFilename { get; set; }
        // タグパーサー
        public bool limitSymbolsToIncludedHeaders { get; set; }
    
        /// <summary>
        /// コンストラクタ(引数無し)
        /// </summary>
        public Browse() {}

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="path">ヘッダファイルのパス</param>
        /// <param name="databaseFilename">データベースへのパス</param>
        /// <param name="limitSymbolsToIncludedHeaders">タグパーサー</param>
        public Browse(string[] path, string databaseFilename, bool limitSymbolsToIncludedHeaders)
        {
            this.path = path;
            this.databaseFilename = databaseFilename;
            this.limitSymbolsToIncludedHeaders = limitSymbolsToIncludedHeaders;
        }
    }
}
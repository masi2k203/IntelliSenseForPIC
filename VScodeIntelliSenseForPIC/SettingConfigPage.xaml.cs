﻿using Microsoft.Win32;
using Microsoft.VisualBasic;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using DialogResult = System.Windows.Forms.DialogResult;
using FolderBrowserDialog = System.Windows.Forms.FolderBrowserDialog;

namespace VScodeIntelliSenseForPIC
{
    /// <summary>
    /// SettingConfigPage.xaml の相互作用ロジック
    /// </summary>
    public partial class SettingConfigPage : Page
    {
        // コンフィグ設定保存インスタンス
        private ConfigProperties configProperties = new ConfigProperties();

        public SettingConfigPage()
        {
            InitializeComponent();
        }

        // < イベントハンドラ > //

        /// <summary>
        /// [参照]ボタンを押した時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileSelectPath_Button_Click(object sender, RoutedEventArgs e)
        {
            // インクルードパス取得
            string includepath = GetFolderPath();

            // テキストボックスへ設定
            ConfigPath2_TextBox.Text = includepath;
            ConfigPath3_TextBox.Text = GetIncludeC99Path(includepath);
        }

        /// <summary>
        /// 拡張設定：[参照]ボタンを押した時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileSelectDatabase_Button_Click(object sender, RoutedEventArgs e)
        {
            // データベースパス取得
            string databasepath = GetFolderPath();

            // テキストボックスへ設定
            DatabaseFileName_TextBox.Text = databasepath;
        }

        /// <summary>
        /// [出力]ボタンを押した時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfigGenerate_Button_Click(object sender, RoutedEventArgs e)
        {
            // コンフィグ情報の設定
            configProperties = SetConfigurations();

            // データシリアライザの生成
            DataSerializer<ConfigProperties> serializer = new DataSerializer<ConfigProperties>();

            // データシリアライズ
            string jsonstring = serializer.SerializeToJson(configProperties);

            if (OutputToDialog_RadioButton.IsChecked == true)
            {
                MessageBox.Show(jsonstring);
            }
            else if (OutputToText_RadioButton.IsChecked == true)
            {
                DoFileSave(jsonstring, "txt");
            }
            else if (OutputToJson_RadioButton.IsChecked == true)
            {
                DoFileSave(jsonstring, "json");
            }
            else
            {
                MessageBox.Show("出力方法を指定してください。");
            }
        }

        /// <summary>
        /// [確定]ボタンを押した時の処理
        /// 型番からプリプロセッサ定義を生成する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ModelNumberSelectButton_Click(object sender, RoutedEventArgs e)
        {
            // 型番を取得
            string defineLabal = ModelNumberTextBox.Text;

            // 大文字に変換
            defineLabal = defineLabal.ToUpper();
            // 半角に変更
            defineLabal = Strings.StrConv(defineLabal, VbStrConv.Narrow);
            // 先頭にアンダーバーを設定
            defineLabal = $"_{defineLabal}";

            // プリプロセッサ定義として設定
            ConfigDefine3_TextBox.Text = defineLabal;
        }

        /// <summary>
        /// 拡張設定を有効化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToggleAdvancedSetting_MenuItem_Checked(object sender, RoutedEventArgs e)
        {
            AdvancedSetting_GroupBox.IsEnabled = true;
        }

        /// <summary>
        /// 拡張設定を無効化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToggleAdvancedSetting_MenuItem_Unchecked(object sender, RoutedEventArgs e)
        {
            AdvancedSetting_GroupBox.IsEnabled = false;
        }



        // < データ処理メソッド > //

        /// <summary>
        /// フォルダ選択ダイアログを生成し、フォルダのパスを取得するメソッド
        /// </summary>
        /// <returns>フォルダパス</returns>
        private string GetFolderPath()
        {
            // パス保存文字列
            string folderpath;

            // 例外処理
            try
            {
                // フォルダ選択ダイアログインスタンス生成
                using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
                {
                    // 説明文
                    folderBrowserDialog.Description = "フォルダを選択してください";
                    // 新しいフォルダの作成を禁止
                    folderBrowserDialog.ShowNewFolderButton = false;
                    // 初期選択位置を指定する
                    folderBrowserDialog.SelectedPath = "C:";

                    // ダイアログ表示
                    DialogResult dialogResult = folderBrowserDialog.ShowDialog();
                    if (dialogResult == DialogResult.Cancel)
                    {
                        // キャンセル => 終了
                        return string.Empty;
                    }

                    // 選択されたパスを取得
                    folderpath = folderBrowserDialog.SelectedPath;

                    return folderpath;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                return string.Empty;
            }
        }


        /// <summary>
        /// c99付きのファイルパスを取得するメソッド
        /// </summary>
        /// <param name="path">インクルードパス</param>
        /// <returns>c99インクルードパス</returns>
        private string GetIncludeC99Path(string path)
        {
            return $"{path}\\c99";
        }

        /// <summary>
        /// コンフィグの値を設定するメソッド
        /// </summary>
        /// <param name="configProperties"></param>
        /// <param name="configurations"></param>
        /// <param name="browse"></param>
        private ConfigProperties SetConfigurations()
        {
            // 一時インスタンスの作成
            ConfigProperties configProperties = new ConfigProperties();
            Configurations[] configurations = new Configurations[] {new Configurations()};
            Browse browse = new Browse();

            // インクルードパスの取得 => 配列に格納
            string[] includepath = new string[]
            {
                ConfigPath1_TextBox.Text,
                ConfigPath2_TextBox.Text,
                ConfigPath3_TextBox.Text
            };

            // プリプロセッサ定義の取得 => 配列に格納
            string[] defines = new string[]
            {
                ConfigDefine1_TextBox.Text,
                ConfigDefine2_TextBox.Text,
                ConfigDefine3_TextBox.Text
            };


            // 各値を設定
            configurations[0].name             = Strings.StrConv(ConfigName_TextBox.Text, VbStrConv.Narrow);
            configurations[0].includePath      = includepath;
            configurations[0].defines          = defines;
            configurations[0].intelliSenseMode = ConfigEngine_ComboBox.Text;
            configurations[0].cStandard        = ConfigCversion_ComboBox.Text;
            configurations[0].cppStandard      = ConfigCppversion_ComboBox.Text;

            browse.path                          = includepath;
            browse.databaseFilename              = DatabaseFileName_TextBox.Text;
            browse.limitSymbolsToIncludedHeaders = ((bool)TagParser_CheckBox.IsChecked) ? true : false;

            // 全ての値を設定
            configurations[0].browse = browse;
            configProperties.SetConfigProperties(configurations, 4);

            // 返却
            return configProperties;
        }

        /// <summary>
        /// ファイルを保存するダイアログを表示、保存を行うメソッド
        /// </summary>
        /// <param name="data">保存するデータ文字列</param>
        /// <param name="extention">拡張子</param>
        private void DoFileSave(string data, string extention)
        {
            // ファイル保存パス
            string savefilepath = string.Empty;

            // ダイアログインスタンスを生成
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            // ファイルの種類を設定
            switch (extention)
            {
                case "json":
                    saveFileDialog.Filter = "JavaScript Object Notation(*.json)|*.json";
                    break;
                case "txt":
                    saveFileDialog.Filter = "テキストファイル(*.txt)|*.txt";
                    break;
                default:
                    saveFileDialog.Filter = "テキストファイル(*.txt)|*.txt";
                    break;
            }

            // ダイアログを表示
            if (saveFileDialog.ShowDialog() == true)
            {
                // 選択されたファイル名を取得
                savefilepath = saveFileDialog.FileName;

                // テキスト出力
                using (var writer = new StreamWriter(savefilepath))
                {
                    writer.WriteLine(data);
                }

                MessageBox.Show($"出力完了\nパス：{savefilepath}");
            }
        }


        // < XC8テンプレート > //

        /// <summary>
        /// テンプレート
        /// XC8 V2.10
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InsertTemplateXC8v210_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.ConfigName_TextBox.Text  = "XC8-2.10-C99";
            this.ConfigPath2_TextBox.Text = "C:\\Program Files (x86)\\Microchip\\xc8\\v2.10\\pic\\include";
            this.ConfigPath3_TextBox.Text = GetIncludeC99Path("C:\\Program Files (x86)\\Microchip\\xc8\\v2.10\\pic\\include");
        }

    }
}

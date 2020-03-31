﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FolderBrowserDialog = System.Windows.Forms.FolderBrowserDialog;
using DialogResult = System.Windows.Forms.DialogResult;

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
        /// [出力]ボタンを押した時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfigGenerate_Button_Click(object sender, RoutedEventArgs e)
        {
            // 出力方法取得
            int outputselect = 0;

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
                outputselect = 2;
            }
            else if (OutputToJson_RadioButton.IsChecked == true)
            {
                outputselect = 3;
            }

            MessageBox.Show($"{outputselect}");
            
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
            Configurations[] configurations = new Configurations[]
                {new Configurations()};
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
            configurations[0].name             = ConfigName_TextBox.Text;
            configurations[0].includePath      = includepath;
            configurations[0].defines          = defines;
            configurations[0].intelliSenseMode = ConfigEngine_ComboBox.Text;
            configurations[0].cStandard        = ConfigCversion_ComboBox.Text;
            configurations[0].cppStandard      = ConfigCppversion_ComboBox.Text;

            browse.path                          = includepath;
            browse.databaseFilename              = "";
            browse.limitSymbolsToIncludedHeaders = true;

            // 全ての値を設定
            configurations[0].browse = browse;
            configProperties.SetConfigProperties(configurations, 4);

            // 返却
            return configProperties;
        }
    }
}

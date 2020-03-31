namespace VScodeIntelliSenseForPIC
{
    /// <summary>
    /// c_cpp_properties.json�̐ݒ���e�N���X
    /// </summary>
    public class ConfigProperties
    {
        // configurations�ȉ��̐ݒ���e
        public Configurations[] configurations { get; set; }
        // version
        public int version { get; set; }

        /// <summary>
        /// �R���X�g���N�^(��������)
        /// </summary>
        public ConfigProperties() {}

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="data">configurations�ȉ��̐ݒ�I�u�W�F�N�g</param>
        /// <param name="version">version</param>
        public ConfigProperties(Configurations[] data, int version)
        {
            this.configurations = data;
            this.version = version;
        }
    }

    /// <summary>
    /// configurations�ȉ��̐ݒ���e�N���X
    /// </summary>
    public class Configurations
    {
        // name:���ʖ�
        public string name { get; set; }
        // includePath:�C���N���[�h�p�X
        public string[] includePath { get; set; }
        // defines:�v���v���Z�b�T��`
        public string[] defines { get; set; }
        // intelliSenseMode:IntelliSense���[�h
        public string intelliSenseMode { get; set; }
        // browse:IntelliSense�p�C���N���[�h�p�X���̐ݒ�
        public Browse browse { get; set; }
        // cStandard:C����W���o�[�W����
        public string cStandard { get; set; }
        // cppStandard:C++����W���o�[�W����
        public string cppStandard { get; set; }

        /// <summary>
        /// �R���X�g���N�^(��������)
        /// </summary>
        public Configurations() {}

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="name">���ʖ�</param>
        /// <param name="includePath">�C���N���[�h�p�X</param>
        /// <param name="defines">�v���v���Z�b�T��`</param>
        /// <param name="intelliSenseMode">IntelliSense���[�h</param>
        /// <param name="browse">IntelliSense�p�ݒ�</param>
        /// <param name="cStandard">C����W���o�[�W����</param>
        /// <param name="cppStandard">C++����W���o�[�W����</param>
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
    /// IntelliSense�ݒ�p�N���X
    /// </summary>
    public class Browse
    {
        // �w�b�_�t�@�C���p�X
        public string[] path { get; set; }
        // �f�[�^�x�[�X�ւ̃p�X
        public string databaseFilename { get; set; }
        // �^�O�p�[�T�[
        public bool limitSymbolsToIncludedHeaders { get; set; }
    
        /// <summary>
        /// �R���X�g���N�^(��������)
        /// </summary>
        public Browse() {}

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="path">�w�b�_�t�@�C���̃p�X</param>
        /// <param name="databaseFilename">�f�[�^�x�[�X�ւ̃p�X</param>
        /// <param name="limitSymbolsToIncludedHeaders">�^�O�p�[�T�[</param>
        public Browse(string[] path, string databaseFilename, bool limitSymbolsToIncludedHeaders)
        {
            this.path = path;
            this.databaseFilename = databaseFilename;
            this.limitSymbolsToIncludedHeaders = limitSymbolsToIncludedHeaders;
        }
    }
}
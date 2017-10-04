using Gboxt.Common.DataAccess.Schemas;

namespace Gboxt.Common.WpfMvvmBase.Commands
{
    /// <summary>
    /// ��ʾһ������������
    /// </summary>
    public class CommandConfig : SimpleConfig, ICommandItem
    {
        /// <summary>
        ///     ����ʾΪ��ť
        /// </summary>
        public bool NoButton
        {
            get;
            set;
        }

        /// <summary>
        ///     ͼ��
        /// </summary>
        public string IconName
        {
            get;
            set;
        }

        /// <summary>
        ///     ֻ�ܵ�������
        /// </summary>
        public bool Signle
        {
            get;
            set;
        }

        /// <summary>
        ///     ����
        /// </summary>
        public string Catalog
        {
            get;
            set;
        }
        /// <summary>
        ///     Ŀ������
        /// </summary>
        public string SourceType
        {
            get;
            set;
        }

    }
}
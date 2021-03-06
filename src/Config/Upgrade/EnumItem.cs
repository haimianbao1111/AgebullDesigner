﻿/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2017/7/12 22:06:39*/
/*****************************************************
©2008-2017 Copy right by agebull.hu(胡天水)
作者:agebull.hu(胡天水)
工程:Agebull.Common.Config
建立:2014-12-03
修改:2017-07-12
*****************************************************/

using System.ComponentModel;
using System.Runtime.Serialization;

using Newtonsoft.Json;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    /// 枚举值节点
    /// </summary>
    [DataContract,JsonObject(MemberSerialization.OptIn)]
    public partial class EnumItem : ConfigBase
    {
        #region 构造
        
        /// <summary>
        /// 构造
        /// </summary>
        public EnumItem()
        {
        }

        #endregion

 
        #region 数据模型

        /// <summary>
        /// 值
        /// </summary>
        [DataMember,JsonProperty("Value", NullValueHandling = NullValueHandling.Ignore)]
        internal string _value;

        /// <summary>
        /// 值
        /// </summary>
        /// <remark>
        /// 值
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"数据模型"),DisplayName(@"值"),Description("值")]
        public string Value
        {
            get
            {
                return _value;
            }
            set
            {
                if(_value == value)
                    return;
                BeforePropertyChanged(nameof(Value), _value,value);
                _value = value;
                OnPropertyChanged(nameof(Value));
            }
        } 
        #endregion

    }
}
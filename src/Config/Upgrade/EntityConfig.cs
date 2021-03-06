﻿/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2017/7/12 23:16:38*/
/*****************************************************
©2008-2017 Copy right by agebull.hu(胡天水)
作者:agebull.hu(胡天水)
工程:Agebull.Common.Config
建立:2014-12-03
修改:2017-07-12
*****************************************************/

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;

using Newtonsoft.Json;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    /// 实体配置
    /// </summary>
    [DataContract,JsonObject(MemberSerialization.OptIn)]
    public partial class EntityConfig : ParentConfigBase
    {
        #region 构造
        
        /// <summary>
        /// 构造
        /// </summary>
        public EntityConfig()
        {
        }

        #endregion

 
        #region 系统

        /// <summary>
        /// 阻止编辑
        /// </summary>
        [DataMember,JsonProperty("DenyScope", NullValueHandling = NullValueHandling.Ignore)]
        internal AccessScopeType _denyScope;

        /// <summary>
        /// 阻止编辑
        /// </summary>
        /// <remark>
        /// 阻止使用的范围
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"系统"),DisplayName(@"阻止编辑"),Description("阻止使用的范围")]
        public AccessScopeType DenyScope
        {
            get
            {
                return _denyScope;
            }
            set
            {
                if(_denyScope == value)
                    return;
                BeforePropertyChanged(nameof(DenyScope), _denyScope,value);
                _denyScope = value;
                OnPropertyChanged(nameof(DenyScope));
            }
        }

        /// <summary>
        /// 最大字段标识号
        /// </summary>
        [DataMember,JsonProperty("MaxIdentity", NullValueHandling = NullValueHandling.Ignore)]
        internal int _maxIdentity;

        /// <summary>
        /// 最大字段标识号
        /// </summary>
        /// <remark>
        /// 最大字段标识号
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"系统"),DisplayName(@"最大字段标识号"),Description("最大字段标识号")]
        public int MaxIdentity
        {
            get
            {
                return _maxIdentity;
            }
            set
            {
                if(_maxIdentity == value)
                    return;
                BeforePropertyChanged(nameof(MaxIdentity), _maxIdentity,value);
                _maxIdentity = value;
                OnPropertyChanged(nameof(MaxIdentity));
            }
        } 
        #endregion 
        #region 数据标识

        /// <summary>
        /// 是否存在属性组合唯一值
        /// </summary>
        /// <remark>
        /// 是否存在属性组合唯一值
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"数据标识"),DisplayName(@"是否存在属性组合唯一值"),Description("是否存在属性组合唯一值")]
        public bool IsUniqueUnion=>Properties.Count(p => p.UniqueIndex > 0) > 1;

        /// <summary>
        /// 主键字段
        /// </summary>
        /// <remark>
        /// 主键字段
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"数据标识"),DisplayName(@"主键字段"),Description("主键字段")]
        public PropertyConfig PrimaryColumn=>Properties.FirstOrDefault(p => p.IsPrimaryKey);

        /// <summary>
        /// 主键字段
        /// </summary>
        /// <remark>
        /// 主键字段
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"数据标识"),DisplayName(@"主键字段"),Description("主键字段")]
        public string PrimaryField => PrimaryColumn?.Name;

        /// <summary>
        /// Redis唯一键模板
        /// </summary>
        [DataMember,JsonProperty("RedisKey", NullValueHandling = NullValueHandling.Ignore)]
        internal string _redisKey;

        /// <summary>
        /// Redis唯一键模板
        /// </summary>
        /// <remark>
        /// 保存在Redis中使用的键模板
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"数据标识"),DisplayName(@"Redis唯一键模板"),Description("保存在Redis中使用的键模板")]
        public string RedisKey
        {
            get
            {
                return _redisKey;
            }
            set
            {
                if(_redisKey == value)
                    return;
                BeforePropertyChanged(nameof(RedisKey), _redisKey,value);
                _redisKey = value;
                OnPropertyChanged(nameof(RedisKey));
            }
        } 
        #endregion 
        #region 数据模型

        /// <summary>
        /// 实体名称
        /// </summary>
        [DataMember,JsonProperty("EntityName", NullValueHandling = NullValueHandling.Ignore)]
        internal string _entityName;

        /// <summary>
        /// 实体名称
        /// </summary>
        /// <remark>
        /// 实体名称
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"数据模型"),DisplayName(@"实体名称"),Description("实体名称")]
        public string EntityName
        {
            get
            {
                return _entityName ?? Name;
            }
            set
            {
                if(_entityName == value)
                    return;
                BeforePropertyChanged(nameof(EntityName), _entityName,value);
                _entityName = value;
                OnPropertyChanged(nameof(EntityName));
            }
        }

        /// <summary>
        /// 字段列表
        /// </summary>
        [DataMember,JsonProperty("_properties", NullValueHandling = NullValueHandling.Ignore)]
        internal ConfigCollection<PropertyConfig> _properties;

        /// <summary>
        /// 字段列表
        /// </summary>
        /// <remark>
        /// 字段列表
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"数据模型"),DisplayName(@"字段列表"),Description("字段列表")]
        public ConfigCollection<PropertyConfig> Properties
        {
            get
            {
                if (_properties != null)
                    return _properties;
                _properties = new ConfigCollection<PropertyConfig>();
                OnPropertyChanged(nameof(Properties));
                return _properties;
            }
            set
            {
                if(_properties == value)
                    return;
                BeforePropertyChanged(nameof(Properties), _properties,value);
                _properties = value;
                OnPropertyChanged(nameof(Properties));
            }
        }

        /// <summary>
        /// 模型
        /// </summary>
        [DataMember,JsonProperty("ModelInclude", NullValueHandling = NullValueHandling.Ignore)]
        internal string _modelInclude;

        /// <summary>
        /// 模型
        /// </summary>
        /// <remark>
        /// 模型
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"数据模型"),DisplayName(@"模型"),Description("模型")]
        public string ModelInclude
        {
            get
            {
                return _modelInclude;
            }
            set
            {
                if(_modelInclude == value)
                    return;
                BeforePropertyChanged(nameof(ModelInclude), _modelInclude,value);
                _modelInclude = value;
                OnPropertyChanged(nameof(ModelInclude));
            }
        }

        /// <summary>
        /// 基类
        /// </summary>
        [DataMember,JsonProperty("ModelBase", NullValueHandling = NullValueHandling.Ignore)]
        internal string _modelBase;

        /// <summary>
        /// 基类
        /// </summary>
        /// <remark>
        /// 模型
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"数据模型"),DisplayName(@"基类"),Description("模型")]
        public string ModelBase
        {
            get
            {
                return _modelBase;
            }
            set
            {
                if(_modelBase == value)
                    return;
                BeforePropertyChanged(nameof(ModelBase), _modelBase,value);
                _modelBase = value;
                OnPropertyChanged(nameof(ModelBase));
            }
        }

        /// <summary>
        /// 数据版本
        /// </summary>
        [DataMember,JsonProperty("_dataVersion", NullValueHandling = NullValueHandling.Ignore)]
        internal int _dataVersion;

        /// <summary>
        /// 数据版本
        /// </summary>
        /// <remark>
        /// 数据版本
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"数据模型"),DisplayName(@"数据版本"),Description("数据版本")]
        public int DataVersion
        {
            get
            {
                return _dataVersion;
            }
            set
            {
                if(_dataVersion == value)
                    return;
                BeforePropertyChanged(nameof(DataVersion), _dataVersion,value);
                _dataVersion = value;
                OnPropertyChanged(nameof(DataVersion));
            }
        }

        /// <summary>
        /// 内部数据
        /// </summary>
        [DataMember,JsonProperty("_isInternal", NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isInternal;

        /// <summary>
        /// 内部数据
        /// </summary>
        /// <remark>
        /// 服务器内部数据,即只在服务器内部使用
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"数据模型"),DisplayName(@"内部数据"),Description("服务器内部数据,即只在服务器内部使用")]
        public bool IsInternal
        {
            get
            {
                return _isInternal;
            }
            set
            {
                if(_isInternal == value)
                    return;
                BeforePropertyChanged(nameof(IsInternal), _isInternal,value);
                _isInternal = value;
                OnPropertyChanged(nameof(IsInternal));
            }
        }

        /// <summary>
        /// 是否类
        /// </summary>
        [DataMember,JsonProperty("IsClass", NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isClass;

        /// <summary>
        /// 是否类
        /// </summary>
        /// <remark>
        /// 是否类，即无数据访问的对象
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"数据模型"),DisplayName(@"是否类"),Description("是否类，即无数据访问的对象")]
        public bool IsClass
        {
            get
            {
                return _isClass;
            }
            set
            {
                if(_isClass == value)
                    return;
                BeforePropertyChanged(nameof(IsClass), _isClass,value);
                _isClass = value;
                OnPropertyChanged(nameof(IsClass));
            }
        }

        /// <summary>
        /// 继承的接口集合
        /// </summary>
        [DataMember,JsonProperty("Interfaces", NullValueHandling = NullValueHandling.Ignore)]
        internal string _interfaces;

        /// <summary>
        /// 继承的接口集合
        /// </summary>
        /// <remark>
        /// 说明
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"数据模型"),DisplayName(@"继承的接口集合"),Description("说明")]
        public string Interfaces
        {
            get
            {
                return _interfaces;
            }
            set
            {
                if(_interfaces == value)
                    return;
                BeforePropertyChanged(nameof(Interfaces), _interfaces,value);
                _interfaces = value;
                OnPropertyChanged(nameof(Interfaces));
            }
        } 
        #endregion 
        #region 项目管理

        /// <summary>
        /// 上级项目
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        internal ProjectConfig _parent;

        /// <summary>
        /// 上级项目
        /// </summary>
        /// <remark>
        /// 上级项目
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"项目管理"),DisplayName(@"上级项目"),Description("上级项目")]
        public ProjectConfig Parent
        {
            get
            {
                return _parent;
            }
            set
            {
                if(_parent == value)
                    return;
                BeforePropertyChanged(nameof(Parent), _parent,value);
                _parent = value;
                OnPropertyChanged(nameof(Parent));
            }
        } 
        #endregion 
        #region 设计器支持

        /// <summary>
        /// 子级(继承)
        /// </summary>
        /// <remark>
        /// 子级(继承)
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"设计器支持"),DisplayName(@"子级(继承)"),Description("子级(继承)")]
        public override IEnumerable<ConfigBase> MyChilds => _properties;

        /// <summary>
        /// 可用字段集合
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        internal ObservableCollection<PropertyConfig> _lastProperties;

        /// <summary>
        /// 可用字段集合
        /// </summary>
        /// <remark>
        /// 可用字段集合,排除了废弃字段
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"设计器支持"),DisplayName(@"可用字段集合"),Description("可用字段集合,排除了废弃字段")]
        public ObservableCollection<PropertyConfig> LastProperties
        {
            get
            {
                if (_lastProperties != null)
                    return _lastProperties;
                _lastProperties = new ObservableCollection<PropertyConfig>();
                OnPropertyChanged(nameof(LastProperties));
                return _lastProperties;
            }
            set
            {
                if(_lastProperties == value)
                    return;
                BeforePropertyChanged(nameof(LastProperties), _lastProperties,value);
                _lastProperties = value;
                OnPropertyChanged(nameof(LastProperties));
            }
        }

        /// <summary>
        /// 字段
        /// </summary>
        [DataMember,JsonProperty("_tableReleations", NullValueHandling = NullValueHandling.Ignore)]
        internal ObservableCollection<TableReleation> _releations;

        /// <summary>
        /// 字段
        /// </summary>
        /// <remark>
        /// 字段
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"设计器支持"),DisplayName(@"字段"),Description("字段")]
        public ObservableCollection<TableReleation> Releations
        {
            get
            {
                if (_releations != null)
                    return _releations;
                _releations = new ObservableCollection<TableReleation>();
                OnPropertyChanged(nameof(Releations));
                return _releations;
            }
            set
            {
                if(_releations == value)
                    return;
                BeforePropertyChanged(nameof(Releations), _releations,value);
                _releations = value;
                OnPropertyChanged(nameof(Releations));
            }
        }

        /// <summary>
        /// 列序号起始值
        /// </summary>
        [DataMember,JsonProperty("ColumnIndexStart", NullValueHandling = NullValueHandling.Ignore)]
        internal int _columnIndexStart;

        /// <summary>
        /// 列序号起始值
        /// </summary>
        /// <remark>
        /// 列序号起始值
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"设计器支持"),DisplayName(@"列序号起始值"),Description("列序号起始值")]
        public int ColumnIndexStart
        {
            get
            {
                return _columnIndexStart;
            }
            set
            {
                if(_columnIndexStart == value)
                    return;
                BeforePropertyChanged(nameof(ColumnIndexStart), _columnIndexStart,value);
                _columnIndexStart = value;
                OnPropertyChanged(nameof(ColumnIndexStart));
            }
        }

        /// <summary>
        /// 项目的说明文字
        /// </summary>
        const string Project_Description = @"存在于哪个项目,用于键盘录入来改变项目归属";

        /// <summary>
        /// 项目
        /// </summary>
        [DataMember,JsonProperty("_project", NullValueHandling = NullValueHandling.Ignore)]
        internal string _project;

        /// <summary>
        /// 项目
        /// </summary>
        /// <remark>
        /// 存在于哪个项目,用于键盘录入来改变项目归属
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"设计器支持"),DisplayName(@"项目"),Description(Project_Description)]
        public string Project
        {
            get
            {
                return _project;
            }
            set
            {
                if(_project == value)
                    return;
                BeforePropertyChanged(nameof(Project), _project,value);
                _project = value;
                OnPropertyChanged(nameof(Project));
            }
        }

        /// <summary>
        /// 名称
        /// </summary>
        /// <remark>
        /// 名称
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"设计器支持"),DisplayName(@"名称"),Description("名称")]
        public string DisplayName => $"{Caption}({EntityName}:{ReadTableName}";

        /// <summary>
        /// 公开的属性
        /// </summary>
        /// <remark>
        /// 公开的属性
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"设计器支持"),DisplayName(@"公开的属性"),Description("公开的属性")]
        public IEnumerable<PropertyConfig> PublishProperty => Properties.Where(p => !p.Discard && !p.DbInnerField);

        /// <summary>
        /// C++的属性
        /// </summary>
        /// <remark>
        /// C++的属性
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"设计器支持"),DisplayName(@"C++的属性"),Description("C++的属性")]
        public IEnumerable<PropertyConfig> CppProperty=>Properties.Where(p => !p.Discard && !p.IsMiddleField && !p.DbInnerField && !p.IsSystemField);

        /// <summary>
        /// 客户端可访问的属性
        /// </summary>
        /// <remark>
        /// 客户端可访问的属性
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"设计器支持"),DisplayName(@"客户端可访问的属性"),Description("客户端可访问的属性")]
        public IEnumerable<PropertyConfig> ClientProperty=>Properties.Where(p => !p.Discard && !p.DenyClient && !p.DbInnerField); 

        /// <summary>
        /// 客户端可访问的属性
        /// </summary>
        /// <remark>
        /// 客户端可访问的属性
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"设计器支持"),DisplayName(@"客户端可访问的属性"),Description("客户端可访问的属性")]
        public IEnumerable<PropertyConfig> UserProperty => Properties.Where(p => !p.Discard && !p.DenyClient && !p.DbInnerField && !p.IsSystemField); 

        /// <summary>
        /// 数据库字段
        /// </summary>
        /// <remark>
        /// 数据库字段
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"设计器支持"),DisplayName(@"数据库字段"),Description("数据库字段")]
        public IEnumerable<PropertyConfig> DbFields => Properties.Where(p => !p.Discard && !p.NoStorage);

        /// <summary>
        /// 不同版本读数据的代码
        /// </summary>
        [DataMember,JsonProperty("_readCoreCodes", NullValueHandling = NullValueHandling.Ignore)]
        internal Dictionary<int,string> _readCoreCodes;

        /// <summary>
        /// 不同版本读数据的代码
        /// </summary>
        /// <remark>
        /// 不同版本读数据的代码
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"设计器支持"),DisplayName(@"不同版本读数据的代码"),Description("不同版本读数据的代码")]
        public Dictionary<int,string> ReadCoreCodes
        {
            get
            {
                return _readCoreCodes;
            }
            set
            {
                if(_readCoreCodes == value)
                    return;
                BeforePropertyChanged(nameof(ReadCoreCodes), _readCoreCodes,value);
                _readCoreCodes = value;
                OnPropertyChanged(nameof(ReadCoreCodes));
            }
        }

        /// <summary>
        /// 接口定义
        /// </summary>
        [DataMember,JsonProperty("IsInterface", NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isInterface;

        /// <summary>
        /// 接口定义
        /// </summary>
        /// <remark>
        /// 作为系统的接口的定义
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"设计器支持"),DisplayName(@"接口定义"),Description("作为系统的接口的定义")]
        public bool IsInterface
        {
            get
            {
                return _isInterface;
            }
            set
            {
                if(_isInterface == value)
                    return;
                BeforePropertyChanged(nameof(IsInterface), _isInterface,value);
                _isInterface = value;
                OnPropertyChanged(nameof(IsInterface));
            }
        } 
        #endregion 
        #region 业务模型

        /// <summary>
        /// 命令集合
        /// </summary>
        [DataMember,JsonProperty("_commands", NullValueHandling = NullValueHandling.Ignore)]
        internal ObservableCollection<UserCommandConfig> _commands;

        /// <summary>
        /// 命令集合
        /// </summary>
        /// <remark>
        /// 命令集合,数据模型中可调用的命令
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"业务模型"),DisplayName(@"命令集合"),Description("命令集合,数据模型中可调用的命令")]
        public ObservableCollection<UserCommandConfig> Commands
        {
            get
            {
                if (_commands != null)
                    return _commands;
                _commands = new ObservableCollection<UserCommandConfig>();
                OnPropertyChanged(nameof(Commands));
                return _commands;
            }
            set
            {
                if(_commands == value)
                    return;
                BeforePropertyChanged(nameof(Commands), _commands,value);
                _commands = value;
                OnPropertyChanged(nameof(Commands));
            }
        } 
        #endregion 
        #region 数据库

        /// <summary>
        /// 存储表名(结果确定)的说明文字
        /// </summary>
        const string SaveTable_Description = @"存储表名,即实体对应的数据库表.因为模型可能直接使用视图,但增删改还在基础的表中时行,而不在视图中时行";

        /// <summary>
        /// 存储表名(结果确定)
        /// </summary>
        /// <remark>
        /// 存储表名,即实体对应的数据库表.因为模型可能直接使用视图,但增删改还在基础的表中时行,而不在视图中时行
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"数据库"),DisplayName(@"存储表名(结果确定)"),Description(SaveTable_Description)]
        public string SaveTable => string.IsNullOrWhiteSpace(_saveTableName) ? _readTableName : _saveTableName;

        /// <summary>
        /// 存储表名(设计录入)的说明文字
        /// </summary>
        const string ReadTableName_Description = @"存储表名,即实体对应的数据库表.因为模型可能直接使用视图,但增删改还在基础的表中时行,而不在视图中时行";

        /// <summary>
        /// 存储表名(设计录入)
        /// </summary>
        [DataMember,JsonProperty("_tableName", NullValueHandling = NullValueHandling.Ignore)]
        internal string _readTableName;

        /// <summary>
        /// 存储表名(设计录入)
        /// </summary>
        /// <remark>
        /// 存储表名,即实体对应的数据库表.因为模型可能直接使用视图,但增删改还在基础的表中时行,而不在视图中时行
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"数据库"),DisplayName(@"存储表名(设计录入)"),Description(ReadTableName_Description)]
        public string ReadTableName
        {
            get
            {
                return _readTableName;
            }
            set
            {
                if(_readTableName == value)
                    return;
                BeforePropertyChanged(nameof(ReadTableName), _readTableName,value);
                _readTableName = value;
                OnPropertyChanged(nameof(ReadTableName));
            }
        }

        /// <summary>
        /// 存储表名
        /// </summary>
        [DataMember,JsonProperty("_saveTableName", NullValueHandling = NullValueHandling.Ignore)]
        internal string _saveTableName;

        /// <summary>
        /// 存储表名
        /// </summary>
        /// <remark>
        /// 存储表名
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"数据库"),DisplayName(@"存储表名"),Description("存储表名")]
        public string SaveTableName
        {
            get
            {
                return _saveTableName;
            }
            set
            {
                if(_saveTableName == value)
                    return;
                BeforePropertyChanged(nameof(SaveTableName), _saveTableName,value);
                _saveTableName = value;
                OnPropertyChanged(nameof(SaveTableName));
            }
        }

        /// <summary>
        /// 数据库编号
        /// </summary>
        [DataMember,JsonProperty("_dbIndex", NullValueHandling = NullValueHandling.Ignore)]
        internal int _dbIndex;

        /// <summary>
        /// 数据库编号
        /// </summary>
        /// <remark>
        /// 数据库编号
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"数据库"),DisplayName(@"数据库编号"),Description("数据库编号")]
        public int DbIndex
        {
            get
            {
                return _dbIndex;
            }
            set
            {
                if(_dbIndex == value)
                    return;
                BeforePropertyChanged(nameof(DbIndex), _dbIndex,value);
                _dbIndex = value;
                OnPropertyChanged(nameof(DbIndex));
            }
        }

        /// <summary>
        /// 按修改更新
        /// </summary>
        [DataMember,JsonProperty("UpdateByModified", NullValueHandling = NullValueHandling.Ignore)]
        internal bool _updateByModified;

        /// <summary>
        /// 按修改更新
        /// </summary>
        /// <remark>
        /// 按修改更新
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"数据库"),DisplayName(@"按修改更新"),Description("按修改更新")]
        public bool UpdateByModified
        {
            get
            {
                return _updateByModified;
            }
            set
            {
                if(_updateByModified == value)
                    return;
                BeforePropertyChanged(nameof(UpdateByModified), _updateByModified,value);
                _updateByModified = value;
                OnPropertyChanged(nameof(UpdateByModified));
            }
        } 
        #endregion 
        #region 用户界面

        /// <summary>
        /// 页面文件夹名称
        /// </summary>
        [DataMember,JsonProperty("PageFolder", NullValueHandling = NullValueHandling.Ignore)]
        internal string _pageFolder;

        /// <summary>
        /// 页面文件夹名称
        /// </summary>
        /// <remark>
        /// 页面文件夹名称
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"用户界面"),DisplayName(@"页面文件夹名称"),Description("页面文件夹名称")]
        public string PageFolder
        {
            get
            {
                return _pageFolder;
            }
            set
            {
                if(_pageFolder == value)
                    return;
                BeforePropertyChanged(nameof(PageFolder), _pageFolder,value);
                _pageFolder = value;
                OnPropertyChanged(nameof(PageFolder));
            }
        }

        /// <summary>
        /// 树形界面
        /// </summary>
        [DataMember,JsonProperty("TreeUi", NullValueHandling = NullValueHandling.Ignore)]
        internal bool _treeUi;

        /// <summary>
        /// 树形界面
        /// </summary>
        /// <remark>
        /// 树形界面
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"用户界面"),DisplayName(@"树形界面"),Description("树形界面")]
        public bool TreeUi
        {
            get
            {
                return _treeUi;
            }
            set
            {
                if(_treeUi == value)
                    return;
                BeforePropertyChanged(nameof(TreeUi), _treeUi,value);
                _treeUi = value;
                OnPropertyChanged(nameof(TreeUi));
            }
        }

        /// <summary>
        /// 编辑页面最大化
        /// </summary>
        [DataMember,JsonProperty("MaxForm", NullValueHandling = NullValueHandling.Ignore)]
        internal bool _maxForm;

        /// <summary>
        /// 编辑页面最大化
        /// </summary>
        /// <remark>
        /// 编辑页面最大化
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"用户界面"),DisplayName(@"编辑页面最大化"),Description("编辑页面最大化")]
        public bool MaxForm
        {
            get
            {
                return _maxForm;
            }
            set
            {
                if(_maxForm == value)
                    return;
                BeforePropertyChanged(nameof(MaxForm), _maxForm,value);
                _maxForm = value;
                OnPropertyChanged(nameof(MaxForm));
            }
        }

        /// <summary>
        /// 编辑页面分几列
        /// </summary>
        [DataMember,JsonProperty("FormCloumn", NullValueHandling = NullValueHandling.Ignore)]
        internal int _formCloumn;

        /// <summary>
        /// 编辑页面分几列
        /// </summary>
        /// <remark>
        /// 编辑页面分几列
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"用户界面"),DisplayName(@"编辑页面分几列"),Description("编辑页面分几列")]
        public int FormCloumn
        {
            get
            {
                return _formCloumn;
            }
            set
            {
                if(_formCloumn == value)
                    return;
                BeforePropertyChanged(nameof(FormCloumn), _formCloumn,value);
                _formCloumn = value;
                OnPropertyChanged(nameof(FormCloumn));
            }
        }

        /// <summary>
        /// 列表详细页
        /// </summary>
        [DataMember,JsonProperty("ListDetails", NullValueHandling = NullValueHandling.Ignore)]
        internal bool _listDetails;

        /// <summary>
        /// 列表详细页
        /// </summary>
        /// <remark>
        /// 列表详细页
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"用户界面"),DisplayName(@"列表详细页"),Description("列表详细页")]
        public bool ListDetails
        {
            get
            {
                return _listDetails;
            }
            set
            {
                if(_listDetails == value)
                    return;
                BeforePropertyChanged(nameof(ListDetails), _listDetails,value);
                _listDetails = value;
                OnPropertyChanged(nameof(ListDetails));
            }
        }

        /// <summary>
        /// 主键正序
        /// </summary>
        [DataMember,JsonProperty("NoSort", NullValueHandling = NullValueHandling.Ignore)]
        internal bool _noSort;

        /// <summary>
        /// 主键正序
        /// </summary>
        /// <remark>
        /// 主键正序
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"用户界面"),DisplayName(@"主键正序"),Description("主键正序")]
        public bool NoSort
        {
            get
            {
                return _noSort;
            }
            set
            {
                if(_noSort == value)
                    return;
                BeforePropertyChanged(nameof(NoSort), _noSort,value);
                _noSort = value;
                OnPropertyChanged(nameof(NoSort));
            }
        }

        /// <summary>
        /// 主页面类型
        /// </summary>
        [DataMember,JsonProperty("PanelType", NullValueHandling = NullValueHandling.Ignore)]
        internal PanelType _panelType;

        /// <summary>
        /// 主页面类型
        /// </summary>
        /// <remark>
        /// 主页面类型
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"用户界面"),DisplayName(@"主页面类型"),Description("主页面类型")]
        public PanelType PanelType
        {
            get
            {
                return _panelType;
            }
            set
            {
                if(_panelType == value)
                    return;
                BeforePropertyChanged(nameof(PanelType), _panelType,value);
                _panelType = value;
                OnPropertyChanged(nameof(PanelType));
            }
        } 
        #endregion 
        #region C++

        /// <summary>
        /// C++名称
        /// </summary>
        [DataMember,JsonProperty("CppName", NullValueHandling = NullValueHandling.Ignore)]
        internal string _cppName;

        /// <summary>
        /// C++名称
        /// </summary>
        /// <remark>
        /// C++字段名称
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"C++"),DisplayName(@"C++名称"),Description("C++字段名称")]
        public string CppName
        {
            get
            {
                return _cppName;
            }
            set
            {
                if(_cppName == value)
                    return;
                BeforePropertyChanged(nameof(CppName), _cppName,value);
                _cppName = value;
                OnPropertyChanged(nameof(CppName));
            }
        } 
        #endregion

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using Agebull.EntityModel.Config;
using Agebull.Common.Mvvm;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 实体配置相关模型
    /// </summary>
    public class EntityDesignModel : DesignModelBase
    {
        #region 操作命令

        public EntityDesignModel()
        {
            Catalog = "字段";
            Model = DataModelDesignModel.Current;
            Context = DataModelDesignModel.Current?.Context;
        }

        /// <summary>
        /// 生成命令对象
        /// </summary>
        /// <returns></returns>
        protected override List<CommandItem> CreateCommands()
        {
            List<CommandItem> commands = new List<CommandItem>
            {
                new CommandItem
                {
                    Command = new DelegateCommand(CopyColumns),
                    Name = "复制列",
                    Image = Application.Current.Resources["tree_item"] as ImageSource
                },
                new CommandItem
                {
                    Command = new DelegateCommand(PasteColumns),
                    Name = "粘贴列",
                    Image = Application.Current.Resources["tree_item"] as ImageSource
                },
                new CommandItem
                {
                    NoButton=true,
                    Command = new DelegateCommand(ClearColumns),
                    Name = "清除列",
                    Image = Application.Current.Resources["img_del"] as ImageSource
                },
                new CommandItem
                {
                    NoButton=true,
                    Command = new DelegateCommand(DeleteColumns),
                    Name = "删除所选列",
                    Image = Application.Current.Resources["img_del"] as ImageSource
                }
            };
            CreateCommands(commands);
            var extends = CommandCoefficient.Coefficient(typeof(EntityConfig), Catalog);
            if (extends.Count > 0)
                commands.AddRange(extends);
            return commands;
        }

        #endregion

        /// <summary>
        /// 复制字段
        /// </summary>
        public void CopyColumns()
        {
            Context.CopiedTable = Context.SelectEntity;
            Context.CopyColumns = Context.SelectColumns.OfType<PropertyConfig>().ToList();
            Context.StateMessage = $"复制了{Context.CopyColumns.Count}行";
        }
        /// <summary>
        /// 复制字段
        /// </summary>
        public void PasteColumns()
        {
            if (Context.CopyColumns == null || Context.CopyColumns.Count == 0 || Context.CopiedTable == Context.SelectEntity ||
                Context.SelectEntity == null || Context.CopiedTable == null)
            {
                Context.StateMessage = "没可粘贴的行";
                return;
            }
            var yes = MessageBox.Show(Application.Current.MainWindow, "是否粘贴关系信息?", "粘贴行", MessageBoxButton.YesNo) ==
                      MessageBoxResult.Yes;
            
            PateFields(yes, Context.CopiedTable, Context.SelectEntity, Context.CopyColumns);

            //this.Context.CopiedTable = null;
            //this.Context.CopiedTables.Clear();
            //Context.SelectColumns = null;
            //this.RaisePropertyChanged(() => this.Context.CopiedTableCounts);
        }
        public void ClearColumns()
        {
            if (Context.SelectEntity == null ||
                MessageBox.Show("确认删除所有字段吗?", "对象编辑", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
            {
                return;
            }
            Context.SelectColumns = null;
            Context.SelectEntity.Properties.Clear();
        }

        public void DeleteColumns()
        {
            if (Context.SelectEntity == null ||
                MessageBox.Show("确认删除所选字段吗?", "对象编辑", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
            {
                return;
            }
            foreach (var col in Context.SelectColumns.OfType<PropertyConfig>().ToArray())
            {
                Context.SelectEntity.Properties.Remove(col);
            }
            Context.SelectColumns = null;
        }

        #region 复制

        public void PateFields(bool yes, EntityConfig source, EntityConfig Entity, List<PropertyConfig> columns)
        {
            foreach (var copyColumn in columns)
            {
                PropertyConfig newColumn;
                if (copyColumn.IsPrimaryKey && copyColumn.Name.ToLower() == "id")
                {
                    string name = copyColumn.Parent.SaveTableName ?? copyColumn.Parent.ReadTableName;
                    newColumn = Entity.Properties.FirstOrDefault(
                        p => p.LinkTable == name && p.IsExtendKey);
                }
                else if (copyColumn.IsCaption)
                {
                    string name = copyColumn.Parent.SaveTableName ?? copyColumn.Parent.ReadTableName;
                    newColumn = Entity.Properties.FirstOrDefault(
                        p => p.LinkTable == name && p.IsLinkCaption);
                }
                else
                {
                    newColumn = Entity.Properties.FirstOrDefault(p =>
                        string.Equals(p.Name, copyColumn.Name, StringComparison.OrdinalIgnoreCase)
                        || string.Equals(p.Alias, copyColumn.Name, StringComparison.OrdinalIgnoreCase)
                        || string.Equals(p.Name, copyColumn.Alias, StringComparison.OrdinalIgnoreCase));
                }

                if (newColumn == null)
                {
                    newColumn = new PropertyConfig();
                    newColumn.CopyFrom(copyColumn);

                    if (copyColumn.IsPrimaryKey && copyColumn.Name.ToLower() == "id")
                    {
                        newColumn.Name = copyColumn.Parent.Name + "Id";
                        newColumn.Caption = copyColumn.Parent.Caption + "外键";
                        newColumn.ColumnName = GlobalConfig.SplitWords(newColumn.Name).Select(p => p.ToLower()).LinkToString("_");
                    }
                    else if (copyColumn.IsCaption)
                    {
                        newColumn.Name = copyColumn.Parent.Name;
                        newColumn.Caption = copyColumn.Parent.Caption;
                        newColumn.ColumnName = GlobalConfig.SplitWords(newColumn.Name).Select(p => p.ToLower()).LinkToString("_");
                    }
                    Entity.Properties.Add(newColumn);
                }
                newColumn.Parent = Entity;
                if (yes)
                {
                    newColumn.LinkTable = string.IsNullOrEmpty(copyColumn.LinkTable)
                        ? source.SaveTableName
                        : copyColumn.LinkTable;
                    newColumn.LinkField = string.IsNullOrEmpty(copyColumn.LinkField)
                        ? copyColumn.Name
                        : copyColumn.LinkField;
                    if (copyColumn.IsLinkKey || copyColumn.IsPrimaryKey)
                    {
                        newColumn.IsLinkKey = true;
                    }
                    else
                    {
                        newColumn.IsLinkCaption = copyColumn.IsCaption || copyColumn.IsLinkCaption;
                        newColumn.IsCompute = copyColumn.IsCompute;
                    }
                    newColumn.IsCompute = !newColumn.IsLinkKey;
                }
                else
                {
                    newColumn.IsLinkField = false;
                    newColumn.LinkTable = null;
                    newColumn.IsLinkKey = false;
                    newColumn.IsLinkCaption = false;
                    newColumn.IsUserId = false;
                    newColumn.LinkField = null;
                    newColumn.IsCompute = false;

                }
                newColumn.IsPrimaryKey = false;
                newColumn.IsCaption = false;
            }
            Entity.IsModify = true;
        }

        #endregion
    }

}
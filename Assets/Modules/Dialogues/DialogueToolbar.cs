using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Modules.Dialogues
{
    public sealed class DialogueToolbar : Toolbar
    {
        public DialogueToolbar(DialogueGraphView graphView)
        {
            ObjectField configField = CreateConfigField();

            Button loadButton = CreateButtonLoad(graphView, configField);
            Button saveButton = CreateButtonSave(graphView, configField);
            Button resetButton = CreateButtonReset(graphView, configField);
            
            Add(configField);
            Add(loadButton);
            Add(saveButton);
            Add(resetButton);
        }
        
        private static ObjectField CreateConfigField()
        {
            return new ObjectField("Selected Dialogue")
            {
                objectType = typeof(DialogueConfig),
                allowSceneObjects = false
            };
        }
        
        private Button CreateButtonLoad(DialogueGraphView graphView, ObjectField configField)
        {
            return new Button
            {
                text = "Load Config",
                clickable = new Clickable(() =>
                {
                    DialogueLoader.LoadDialogue(configField.value as DialogueConfig, graphView);
                })
            };
        }
        
        private Button CreateButtonSave(DialogueGraphView graphView, ObjectField configField)
        {
            return new Button
            {
                text = "Save Config",
                clickable = new Clickable(() =>
                {
                    var config = configField.value as DialogueConfig;

                    if (config != null)
                    {
                        DialogueSaver.SaveDialogue(graphView, config);
                    }
                    else
                    {
                        DialogueSaver.SaveDialogueAsNew(graphView, out config);
                        configField.value = config;
                    }
                })
            };
        }
        
        private Button CreateButtonReset(DialogueGraphView graphView, ObjectField configField)
        {
            return new Button
            {
                text = "Reset Dialogue",
                clickable = new Clickable(() =>
                {
                    graphView.ResetState();
                    configField.value = null;
                })
            };
        }
    }
}
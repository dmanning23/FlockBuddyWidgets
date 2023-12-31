﻿using FlockBuddy;
using MenuBuddy;
using System.Threading.Tasks;

namespace FlockBuddyWidgets
{
	public class BehaviorsScreen : BaseTab
	{
		readonly FlockManager _flock;
		IButton _addBehavior;

		public BehaviorsScreen(FlockManager flock) : base("BehaviorsScreen")
		{
			_flock = flock;
		}

		public override async Task LoadContent()
		{
			await base.LoadContent();

			//Add the header
			AddHeader(_flock);

			//add all the behaviors
			foreach (var behavior in _flock.GetAllBehaviors())
			{
				AddBehaviorButton(behavior);
			}

			//add the "Add Behavior" button
			_addBehavior = CreateButton("Add Behavior");
			_addBehavior.OnClick += (obj, e) =>
			{
				//create the popup box
				ScreenManager.AddScreen(new AddBehaviorMessageBox(_flock, this));
			};

			AddItem(ToolStack);
		}

		private void AddBehaviorButton(BehaviorType behavior)
		{
			var flockControl = new BehaviorControl(_flock, this, behavior);

			//add a button control for it
			ToolStack.AddItem(flockControl);

			AddShim();
		}

		public void AddBehavior(BehaviorType behavior)
		{
			_flock.AddBehavior(behavior);

			var flockControl = new BehaviorControl(_flock, this, behavior);
			flockControl.LoadContent(this);

			var shim = AddShim(_addBehavior);

			//add a button control for it
			ToolStack.InsertItemBefore(flockControl, shim);
		}

		public void RemoveBehavior(BehaviorType behavior)
		{
			//remove from the flock collection
			_flock.RemoveBehavior(behavior);

			//remove the widget
			foreach (var item in ToolStack.Items)
			{
				var behaviorItem = item as BehaviorControl;
				if (behaviorItem != null && behaviorItem.Behavior == behavior)
				{
					ToolStack.RemoveItem(behaviorItem);
					return;
				}
			}
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class PlayerTests
    {
        [UnityTest]
        public IEnumerator BulletDestructionCheck()
        {
            var gameObject = new GameObject();
            var bullet = gameObject.AddComponent<AIBullet>();
            //Rigidbody rBod = bullet.GetComponent<Rigidbody>();
            bullet.SelfDestruct();
            yield return new WaitForSeconds(4f);
            Assert.IsTrue(bullet.selfdestructed);

        }

        [UnityTest]
        public IEnumerator CheckNewAddedItem()
        {
            var gameObject = new GameObject();
            //var item = ItemInteract.CreateInstance<ItemInteract>();
            var tmp = gameObject.AddComponent<InventoryPl>();
            var item = ItemInteract.CreateInstance<ItemInteract>();
            //tmp.player = GameObject.Find(("ThirdPersonPlayer")).transform;
            tmp.items = new List<ItemInteract>();
            tmp.addToInv = gameObject.AddComponent<AudioSource>();
            Assert.AreEqual(true, tmp.AddItem(item));
            yield return null;

        }

        [UnityTest]
        public IEnumerator InDialogueCheck()
        {
            var gameObject = new GameObject();
            var convoController = gameObject.AddComponent<ConversationController>();
            var speakerUILeft = gameObject.AddComponent<SpeakerUiController>();
            var speakerUIRight = gameObject.AddComponent<SpeakerUiController>();
            convoController.speakerLeft = speakerUILeft.gameObject;
            convoController.speakerRight = speakerUIRight.gameObject;
            var conversation = Conversation.CreateInstance<Conversation>();
            speakerUILeft.Speaker = conversation.speakerLeft;
            speakerUIRight.Speaker = conversation.speakerRight;
            convoController.Initialize();
            Assert.AreEqual(true, convoController.conversationStarted);
            yield return null;

        }
        [UnityTest]
        public IEnumerator MawTrigeredCheck()
        {
            var gameObject = new GameObject();
            var maw = gameObject.AddComponent<MawTrap>();
            maw.anim = gameObject.AddComponent<Animator>();
            maw.StartCoroutine(maw._TriggerMaw());
            var check = true;
            yield return new WaitForSeconds(.1f);
            Assert.AreEqual(check, maw.mawTriggered);

        }

    }
}

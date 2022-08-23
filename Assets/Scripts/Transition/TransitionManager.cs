using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Transition
{
    
    public class TransitionManager : MonoBehaviour
    {
        public string startSceneName = string.Empty;

        private CanvasGroup fadeCanvasGroup;
        private bool isFade;

        private void OnEnable()
        {
            EventHandler.TransitionEvent += OnTransitionEvent;
        }

        private void OnDisable()
        {
            EventHandler.TransitionEvent -= OnTransitionEvent;
        }
        
        private void Start()
        {
            StartCoroutine(LoadSceneSetActive(startSceneName));
            fadeCanvasGroup = FindObjectOfType<CanvasGroup>();
        }
        
        private void OnTransitionEvent(string sceneToGo, Vector3 positionToGo)
        {
            if(!isFade) 
                StartCoroutine(Transition(sceneToGo, positionToGo));
        }
        
        /// <summary>
        /// 淡入淡出场景
        /// </summary>
        /// <param name="targetAlpha">1是黑0是透明</param>
        /// <returns></returns>
        private IEnumerator Fade(float targetAlpha)
        {
            isFade = true;

            fadeCanvasGroup.blocksRaycasts = true;

            float speed = Mathf.Abs(fadeCanvasGroup.alpha - targetAlpha)/Settings.CanvasFadeDuration;

            while (!Mathf.Approximately(fadeCanvasGroup.alpha,targetAlpha))
            {
                fadeCanvasGroup.alpha = Mathf.MoveTowards(fadeCanvasGroup.alpha, targetAlpha, speed * Time.deltaTime);
                yield return null;
            }

            fadeCanvasGroup.blocksRaycasts = false;

            isFade = false;
        }
        
        /// <summary>
        /// 场景切换
        /// </summary>
        /// <param name="sceneName">目标场景</param>
        /// <param name="targetPosition">目标位置</param>
        /// <returns></returns>
        private IEnumerator Transition(string sceneName,Vector3 targetPosition)
        {
            
            EventHandler.CallBeforeSceneUnloadEvent();
            
            yield return Fade(1);
            
            yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
    
            yield return LoadSceneSetActive(sceneName);
            
            //移动人物位置坐标
            EventHandler.CallMoveToPosition(targetPosition);
            
            //Debug.Log("before call afterload");
            EventHandler.CallAfterSceneLoadedEvent();
            //Debug.Log("after call afterload");
            
            //Debug.Log("before fade");
            yield return Fade(0);
            //Debug.Log("after fade");
        }
        
        /// <summary>
        /// 加载场景并设置为激活
        /// </summary>
        /// <param name="sceneName">场景名</param>
        /// <returns></returns>
        private IEnumerator LoadSceneSetActive(string sceneName)
        {
            yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
    
            Scene newScene = SceneManager.GetSceneAt(SceneManager.sceneCount -1);
    
            SceneManager.SetActiveScene(newScene);
        }


        
    }
}


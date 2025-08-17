pipeline {
    agent any
    
    environment {
        UNITY_PATH = "E:\\Programs\\Unity\\2022.3.7f1\\Editor\\Unity.exe" // CAMBIAD ESTO
        REPO_URL = "https://github.com/GerardGascon/CookieClickerJenkins.git"
    }
    
    stages {
        stage('Checkout') {
            steps {
            	discordSend description: "[Geri]: Something failed", footer: "Aquí footer", link: env.BUILD_URL, result: currentBuild.currentResult, title: "[GERI]: ${env.JOB_NAME}", webhookURL: "https://discord.com/api/webhooks/1403692153439391754/jQaX79xZrL0QqQ4PlwgmUwclwU4Fpriv1yxOowDFKiFPI8wmjoVsjeULtlC7QKFknd9a"
                bat "git pull ${REPO_URL}"
            }
        }
        
        stage('Test') {
            steps {
                bat """
                    if not exist "CI" mkdir "CI"
                    "${UNITY_PATH}" -runTests -projectPath "%WORKSPACE%" -exit -batchmode -testResults "%WORKSPACE%\\CI\\results.xml" -testPlatform EditMode
                """
            }
        }
        
        stage('Build') {
            steps {
                bat """
                    "${UNITY_PATH}" -executeMethod SimpleBuildScript.Build -projectPath "%WORKSPACE%" -quit -batchmode
                """
                archiveArtifacts artifacts: 'Build/**/*', fingerprint: true
            }
        }

        stage('Publish') {
        	steps {
        		bat """
        			butler push "${pwd()}/Build" geri8/jenkins-test:windows
        		"""
        	}
        }
    }
}
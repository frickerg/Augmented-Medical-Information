Shader "Custom/InvisibleMask" {
	SubShader {
		// source: https://answers.unity.com/questions/316064/can-i-obscure-an-object-using-an-invisible-object.html
		// draw after all opaque objects (queue = 3001):
		Tags { "Queue"="Transparent+1"}
		Pass {
			Blend Zero One // keep the image behind it
		}
	}
}
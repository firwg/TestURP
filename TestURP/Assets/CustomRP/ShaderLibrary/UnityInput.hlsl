#ifndef CUSTOM_UNLIT_INPUT_INCLUDED
#define CUSTOM_UNLIT_INPUT_INCLUDED

//局部坐标系转世界坐标系的矩阵
float4x4 unity_ObjectToWorld;

float4x4 unity_WorldToObject;

//从世界坐标系到 投影空间的矩阵
float4x4 unity_MatrixVP;

float4x4 unity_MatrixV;
float4x4 glstate_matrix_projection;


real4 unity_WorldTransformParams;

#endif